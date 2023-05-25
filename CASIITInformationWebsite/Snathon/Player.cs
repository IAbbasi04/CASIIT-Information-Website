using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows;

namespace CasiitCourses
{
    public class Player : GameObject
    {
        public enum MovementDirection
        {
            Left, Right, Up, Down
        }
        public class PlayerSegment : GameObject
        {
            public PlayerSegment MasterSegment { get; set; } // this is the segment that it will follow
            //public TableCell currentCell; // stores the cell this segment was in during the previous reload so its follower can take its place
            public MovementDirection RecentMovement { get; set; } = MovementDirection.Up;
            // stores the most recent movement direction of this segment so that another segment could be added behind it if this segment was a tail
            // recent movement direction defaults to up

            // head segment does not have a master
            // body segments have a master
            public PlayerSegment(Image objectImageFormat, System.Drawing.Point containingCellCoordinate, PlayerSegment masterSegment) : base(objectImageFormat, containingCellCoordinate)
            {
                // know what segment to follow
                MasterSegment = masterSegment;
            }
            public void move(System.Drawing.Point cellCoordinate, MovementDirection direction) // move a segment towards the given direction and set its previous movement
            {
                // add this segment's image to the given cell
                //cell.Controls.Add(ObjectImage);
                // update this segment's previousCell
                ContainingCellCoordinate = cellCoordinate;
                //currentCell = cellCoordinate;

                // update the recent movement direction for the cell
                switch (direction)
                {
                    case MovementDirection.Up:
                        RecentMovement = MovementDirection.Up;
                        break;
                    case MovementDirection.Down:
                        RecentMovement = MovementDirection.Down;
                        break;
                    case MovementDirection.Left:
                        RecentMovement = MovementDirection.Left;
                        break;
                    case MovementDirection.Right:
                        RecentMovement = MovementDirection.Right;
                        break;
                }
            }
        }
        public bool IsAlive { get; set; } = true; // a player starts out as alive, used by Main code to determine if the player should keep moving or not
        public PlayerSegment Head { get => head; set => head = value; }
        private PlayerSegment head; // front segment of the snake (the player directly controls this segment)
        public PlayerSegment Tail { get; set; } // back segement of the snake
        public int Length { get; set; } = 1; // defaults to 1 (there will always at least be a head)
        private Image playerBodyImage; // store an image to apply to newly added segments
        private Table containingTable; // store the table that the snake belongs to

        public Player(Image playerHeadImageFormat, Image playerBodyImageFormat, System.Drawing.Point initialContainingCellCoordinate, Table containingTable) : base() // a collection of PlayerSegments (singly linked list)
        {
            Head = new PlayerSegment(playerHeadImageFormat, initialContainingCellCoordinate, null); // the head does not have a master segment, it follows the player's input
            this.playerBodyImage = playerBodyImageFormat;
            Tail = Head;

            this.containingTable = containingTable;
        }
        public bool tryMoveSnake(MovementDirection playerInputDirection) // move the snake's head towards the player input and its body segments along the exact path the head took
        {
            // move each segment individually, starting from the tail
            PlayerSegment currentSegment = Tail;
            for (int i = 0; i < Length; i++)
            {
                if (currentSegment.MasterSegment != null) // segment has a master and is therefore part of the body
                {
                    // add this segment to its master's previous cell
                    currentSegment.move(currentSegment.MasterSegment.ContainingCellCoordinate, currentSegment.MasterSegment.RecentMovement);
                    // get the next segment
                    currentSegment = currentSegment.MasterSegment;
                }
                else // segment has no master and is therefore the head
                {
                    // get the coordinate of the cell
                    System.Drawing.Point cellCoordinate = head.ContainingCellCoordinate; //GetCellCoordinate(head.currentCell);// head.ContainingCell);
                    
                    // if the length of the snake is greater than one, apply this rule
                    // if the movement direction is opposite of the recent movement, continue going the direction of the recent movement, the snake cannot travel back on itself
                    if (Length > 1)
                    {
                        if (playerInputDirection == GetOppositeDirection(head.RecentMovement)) // if the player input the direction opposite of which it just traveled
                        {
                            playerInputDirection = head.RecentMovement; // set the snake to move in the most recent direction
                        }
                    }

                    // attempt to move segment towards playerInputDirection
                    switch (playerInputDirection)
                    {
                        // coordinate directions are weird (X means row, Y means column)
                        case MovementDirection.Up:
                            cellCoordinate.X--;
                            break;
                        case MovementDirection.Down:
                            cellCoordinate.X++;
                            break;
                        case MovementDirection.Left:
                            cellCoordinate.Y--;
                            break;
                        case MovementDirection.Right:
                            cellCoordinate.Y++;
                            break;
                    }
                    try
                    {
                        // get the cell for the snake to move into
                        TableCell newCell = containingTable.Rows[cellCoordinate.X].Cells[cellCoordinate.Y]; // this will error if the snake tries to travel out of bounds

                        // if the code reaches this point the newCell is in bounds of the table
                        // move the head into its newCell
                        currentSegment.move(cellCoordinate, playerInputDirection);
                    }
                    catch // if snake traveled outside of the table
                    {
                        return false; // tell the calling code that the snake's move was not in bounds of the table
                    }
                }
            }
            return true; // tell the calling code that the snake's moved successfully
        }
        public void addSegment() // add a segment behind the tail
        {
            // find the position behind the tail
            System.Drawing.Point cellCoordinateBehindTail = GetCellCoordinateBehindTail();
            // add new segment behind tail, set this new segment to follow the tail
            PlayerSegment newSegment = new PlayerSegment(playerBodyImage, cellCoordinateBehindTail, Tail);
            // make new segment the tail
            Tail = newSegment;
            // increment length
            Length++;
        }
        public bool TryRemoveSegment() // used when a player is dead in order to remove its segment's from the screen
        {
            if (Length > 0) // the player contains segments
            {
                if (Length == 1) // if the player is only a head
                {
                    // removing the head and tail will remove the knowledge of the player, deleting it from the screen
                    head = null;
                    Tail = null;
                }
                else
                {
                    // store the new tail
                    PlayerSegment newTail = Tail.MasterSegment;
                    // remove the current tail from the player
                    Tail.MasterSegment = null;
                    // set the new tail
                    Tail = newTail;
                }

                // decrement length
                Length--;

                return true; // remove successful
            }
            else // the player contains no segments
            {
                return false; // remove failed
            }
        }
        public void MakeAllPlayerSegmentsTransparent()
        {
            // individually loop through each segment
            PlayerSegment currentSegment = Tail;
            for (int i = 0; i < Length; i++)
            {
                // change its image's transparency
                currentSegment.ObjectImage.CssClass += " DeadPlayerStyle"; // add a css class that makes the image transparent

                // get the next segment
                currentSegment = currentSegment.MasterSegment;
            }
        }
        private System.Drawing.Point GetCellCoordinateBehindTail() // get the position behind the tail.
        {
            // copy the tails position
            //TableCell cell = tail.currentCell;//.ContainingCell;
            // copy the coordinate of the cell
            System.Drawing.Point cellCoordinate = Tail.ContainingCellCoordinate; //GetCellCoordinate(cell);

            switch (Tail.RecentMovement)// the position behind the tail will be the opposite side of tail's previous movement
            {
                // coordinate directions are weird (X means row, Y means column)
                case MovementDirection.Up:
                    cellCoordinate.X++;
                    break;
                case MovementDirection.Down:
                    cellCoordinate.X--;
                    break;
                case MovementDirection.Left:
                    cellCoordinate.Y++;
                    break;
                case MovementDirection.Right:
                    cellCoordinate.Y--;
                    break;
            }
            //TableCell cellBehindTail = containingTable.Rows[cellCoordinate.X].Cells[cellCoordinate.Y];
            return new System.Drawing.Point(cellCoordinate.X, cellCoordinate.Y);
        }
        private MovementDirection GetOppositeDirection(MovementDirection direction)
        {
            switch (direction)
            {
                case MovementDirection.Up:
                    return MovementDirection.Down;
                case MovementDirection.Down:
                    return MovementDirection.Up;
                case MovementDirection.Left:
                    return MovementDirection.Right;
                default: // the direction must be right
                    return MovementDirection.Left;

               // case MovementDirection.Right:
                    //return MovementDirection.Left;
            }
        }
        //private System.Drawing.Point GetCellCoordinate(TableCell cell)
        //{
        //    // loop through every cell in the given table
        //    for (int row = 0; row < containingTable.Rows.Count; row++)
        //    {
        //        // store the current row
        //        TableRow tableRow = containingTable.Rows[row];
        //        for (int col = 0; col < containingTable.Rows[0].Cells.Count; col++)
        //        {
        //            // if the current cell equals the given cell, the cell is in the table
        //            if (tableRow.Cells[col].Equals(cell))
        //            {
        //                return new System.Drawing.Point(row, col);
        //            }
        //        }
        //    }
        //    return new System.Drawing.Point(-1, -1); // this cell does not exist in the table
        //}
    }
}

// STORES CELLS INSTEAD OF CELL COORDINATES

//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Web;
//using System.Web.UI.WebControls;
//using System.Windows;

//namespace CasiitCourses
//{
//    public class Player : GameObject
//    {
//        public enum MovementDirection
//        {
//            Left, Right, Up, Down
//        }
//        public class PlayerSegment : GameObject
//        {
//            public PlayerSegment masterSegment; // this is the segment that it will follow
//            public TableCell currentCell; // stores the cell this segment was in during the previous reload so its follower can take its place
//            public MovementDirection recentMovement = MovementDirection.Up; 
//            // stores the most recent movement direction of this segment so that another segment could be added behind it if this segment was a tail
//            // recent movement direction defaults to up

//            // head segment does not have a master
//            // body segments have a master
//            public PlayerSegment(Image objectImage, TableCell containingCell, PlayerSegment masterSegment) : base(objectImage, containingCell)
//            {
//                // know what segment to follow
//                this.masterSegment = masterSegment;
//            }
//            public void move(TableCell cell, MovementDirection direction) // move a segment towards the given direction and set its previous movement
//            {
//                // add this segment's image to the given cell
//                cell.Controls.Add(ObjectImage);
//                // update this segment's previousCell
//                currentCell = cell;

//                // update the recent movement direction for the cell
//                switch (direction)
//                {
//                    case MovementDirection.Up:
//                        recentMovement = MovementDirection.Up;
//                        break;
//                    case MovementDirection.Down:
//                        recentMovement = MovementDirection.Down;
//                        break;
//                    case MovementDirection.Left:
//                        recentMovement = MovementDirection.Left;
//                        break;
//                    case MovementDirection.Right:
//                        recentMovement = MovementDirection.Right;
//                        break;
//                }
//            }
//        }

//        public PlayerSegment Head { get => head; set => head = value; } 
//        private PlayerSegment head; // front segment of the snake (the player directly controls this segment)
//        private PlayerSegment tail; // back segement of the snake
//        private int length = 1; // defaults to 1 (there will always at least be a head)
//        private Image playerBodyImage; // store an image to apply to newly added segments
//        private Table containingTable; // store the table that the snake belongs to

//        public Player(Image playerHeadImage, Image playerBodyImage, TableCell initialContainingCell, Table containingTable) : base() // a collection of PlayerSegments (singly linked list)
//        {
//            Head = new PlayerSegment(playerHeadImage, initialContainingCell, null); // the head does not have a master segment, it follows the player's input
//            this.playerBodyImage = playerBodyImage;
//            tail = Head;

//            this.containingTable = containingTable;
//        }
//        public bool tryMoveSnake(MovementDirection playerInputDirection) // move the snake's head towards the player input and its body segments along the exact path the head took
//        {
//            // move each segment individually, starting from the tail
//            PlayerSegment currentSegment = tail;
//            for (int i = 0; i < length; i++)
//            {
//                if (currentSegment.masterSegment != null) // segment has a master and is therefore part of the body
//                {
//                    // add this segment to its master's previous cell
//                    currentSegment.move(currentSegment.masterSegment.currentCell, currentSegment.masterSegment.recentMovement);
//                    // get the next segment
//                    currentSegment = currentSegment.masterSegment;
//                }
//                else // segment has no master and is therefore the head
//                {
//                    // get the coordinate of the cell
//                    System.Drawing.Point cellCoordinate = GetCellCoordinate(head.currentCell);// head.ContainingCell);
//                    // attempt to move segment towards playerInputDirection
//                    switch (playerInputDirection)
//                    {
//                        // coordinate directions are weird (X means row, Y means column)
//                        case MovementDirection.Up:
//                            cellCoordinate.X--;
//                            break;
//                        case MovementDirection.Down:
//                            cellCoordinate.X++;
//                            break;
//                        case MovementDirection.Left:
//                            cellCoordinate.Y--;
//                            break;
//                        case MovementDirection.Right:
//                            cellCoordinate.Y++;
//                            break;
//                    }
//                    try
//                    {
//                        // get the cell for the snake to move into
//                        TableCell newCell = containingTable.Rows[cellCoordinate.X].Cells[cellCoordinate.Y]; // this will error if the snake tries to travel out of bounds

//                        // if the code reaches this point the newCell is in bounds of the table
//                        // move the head into its newCell
//                        currentSegment.move(newCell, playerInputDirection);
//                    }
//                    catch // if snake traveled outside of the table
//                    {
//                        return false; // tell the calling code that the snake's move was not in bounds of the table
//                    }
//                }
//            }
//            return true; // tell the calling code that the snake's moved successfully
//        }
//        public void addSegment() // add a segment behind the tail
//        {
//            // find the position behind the tail
//            TableCell cellBehindTail = GetCellBehindTail();
//            // add new segment behind tail, set this new segment to follow the tail
//            PlayerSegment newSegment = new PlayerSegment(playerBodyImage, cellBehindTail, tail);
//            // make new segment the tail
//            tail = newSegment;
//            // increment length
//            length++;
//        }
//        private TableCell GetCellBehindTail() // get the position behind the tail.
//        {
//            // copy the tails position
//            TableCell cell = tail.currentCell;//.ContainingCell;
//            // get the coordinate of the cell
//            System.Drawing.Point cellCoordinate = GetCellCoordinate(cell);

//            switch (tail.recentMovement)// the position behind the tail will be the opposite side of tail's previous movement
//            {
//                // coordinate directions are weird (X means row, Y means column)
//                case MovementDirection.Up:
//                    cellCoordinate.X++;
//                    break;
//                case MovementDirection.Down:
//                    cellCoordinate.X--;
//                    break;
//                case MovementDirection.Left:
//                    cellCoordinate.Y++;
//                    break;
//                case MovementDirection.Right:
//                    cellCoordinate.Y--;
//                    break;
//            }
//            TableCell cellBehindTail = containingTable.Rows[cellCoordinate.X].Cells[cellCoordinate.Y];
//            return cellBehindTail;
//        }
//        private System.Drawing.Point GetCellCoordinate(TableCell cell)
//        {
//            // loop through every cell in the given table
//            for (int row = 0; row < containingTable.Rows.Count; row++)
//            {
//                // store the current row
//                TableRow tableRow = containingTable.Rows[row];
//                for (int col = 0; col < containingTable.Rows[0].Cells.Count; col++)
//                {
//                    // if the current cell equals the given cell, the cell is in the table
//                    if (tableRow.Cells[col].Equals(cell))
//                    {
//                        return new System.Drawing.Point(row, col);
//                    }
//                }
//            }
//            return new System.Drawing.Point(-1, -1); // this cell does not exist in the table
//        }
//    }
//}

// WITH SYSTEM.DRAWING.POINT

//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Web;
//using System.Web.UI.WebControls;
//using System.Windows;

//namespace CasiitCourses
//{
//    public class Player : GameObject
//    {
//        public enum MovementDirection
//        {
//            Left, Right, Up, Down
//        }
//        public class PlayerSegment : GameObject
//        {
//            public PlayerSegment masterSegment; // this is the segment that it will follow
//            public MovementDirection previousMovement; // stores the previous movement so its follower can repeat after it

//            // give each segment an image, position, and master
//            // head segment does not have a master
//            // body segments have a master
//            public PlayerSegment(Image objectImage, System.Drawing.Point position, PlayerSegment masterSegment) : base(objectImage, position)
//            {
//                // know what segment to follow
//                this.masterSegment = masterSegment;
//            }
//            public void move(MovementDirection direction) // move a segment towards the given direction and set its previous movement
//            {
//                switch (direction) // move the current segment
//                {
//                    case MovementDirection.Up:
//                        //Position.Y++;
//                        previousMovement = MovementDirection.Up;
//                        break;
//                    case MovementDirection.Down:
//                        //Position.Y--;
//                        previousMovement = MovementDirection.Down;
//                        break;
//                    case MovementDirection.Left:
//                        //Position.X--;
//                        previousMovement = MovementDirection.Left;
//                        break;
//                    case MovementDirection.Right:
//                        //Position.X++;
//                        previousMovement = MovementDirection.Right;
//                        break;
//                }
//            }
//        }

//        public PlayerSegment Head { get => head; set => head = value; }
//        private PlayerSegment head; // front segment of the snake (the player directly controls this segment)
//        private PlayerSegment tail; // back segement of the snake
//        private int length = 1; // defaults to 1 (there will always at least be a head)
//        private Image playerBodyImage; // store an image to apply to newly added segments

//        public Player(Image playerHeadImage, Image playerBodyImage, System.Drawing.Point startPosition) : base() // a collection of PlayerSegments (singly linked list)
//        {
//            Head = new PlayerSegment(playerHeadImage, startPosition, null); // the head does not have a master segment, it follows the player's input
//            this.playerBodyImage = playerBodyImage;
//            tail = Head;
//        }
//        public void moveSnake(MovementDirection playerInputDirection) // move the snake's head towards the player input and its body segments along the exact path the head took
//        {
//            // move each segment individually starting from the tail
//            PlayerSegment currentSegment = tail;
//            for (int i = 0; i < length; i++)
//            {
//                if (currentSegment.masterSegment != null) // segment has a master and is therefore part of the body
//                {
//                    // move in the direction its master segment previously moved
//                    currentSegment.move(currentSegment.masterSegment.previousMovement);
//                    // get the next segment
//                    currentSegment = currentSegment.masterSegment;
//                }
//                else // segment has no master and is therefore the head
//                {
//                    // move segment towards playerInputDirection
//                    currentSegment.move(playerInputDirection);
//                }
//            }
//        }
//        public void addSegment() // add a segment behind the tail
//        {
//            // find the position behind the tail
//            System.Drawing.Point posBehindTail = GetPositionBehindTail();
//            // add new segment behind tail, set this new segment to follow the tail
//            PlayerSegment newSegment = new PlayerSegment(playerBodyImage, posBehindTail, tail);
//            // make new segment the tail
//            tail = newSegment;
//            // increment length
//            length++;
//        }
//        private System.Drawing.Point GetPositionBehindTail() // get the position behind the tail.
//        {
//            // copy the tails position
//            System.Drawing.Point pos = tail.Position;

//            switch (tail.previousMovement)// the position behind the tail will be the opposite side of tail's previous movement
//            {
//                case MovementDirection.Up:
//                    pos.Y--;
//                    return pos;
//                case MovementDirection.Down:
//                    pos.Y++;
//                    return pos;
//                case MovementDirection.Left:
//                    pos.X++;
//                    return pos;
//                case MovementDirection.Right:
//                    pos.X--;
//                    return pos;
//                default:
//                    return pos; // code should never get here
//            }
//        }
//    }
//}



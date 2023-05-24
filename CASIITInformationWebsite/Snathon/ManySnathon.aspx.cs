using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Input;
using System.Xml.Linq;
using static CasiitCourses.Player;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using Label = System.Web.UI.WebControls.Label;


// player shouldn't immediatley disapear, player controls should not switch upon player death
namespace CasiitCourses
{
    //System.Diagnostics.Debug.WriteLine(HttpContext.Current.Request.Url);
    public partial class ManySnathan : System.Web.UI.Page
    {
        public int Number_of_Players;

        public int pointsCollected = 0; // keep track of the number consumables the snake eats

        private List<Player> players = new List<Player>(); // keep track of all players dead or alive
        //private List<Player> alivePlayers = new List<Player>(); // keep track of the players currently alive for visual display of movement etc.
        private Image playerHeadImageFormat = new Image(); // create a format to apply to a specific object when its created
        private Image playerBodyImageFormat = new Image(); // create a format to apply to a specific object when its created

        private List<Item> items = new List<Item>(); // keep track of the items on the screen
        private Image itemImageFormat = new Image();


        private const int TABLE_HEIGHT = 750;
        private const int TABLE_WIDTH = 750;
        private const int NUMBER_OF_TABLE_ROWS = 20; // when changing the number of rows and columns, you may have to start the game a couple of times in order to get the new gameobject image sizes to be applied
        private const int NUMBER_OF_TABLE_COLUMNS = 20; // otherwise if that doesn't work, you may need to clear your webrowsers cached images (I had to rerun my project 3 times to get it to work)
        private double ROW_HEIGHT;
        private double COLUMN_WIDTH;

        protected void Page_Load(object sender, EventArgs e)
        {
            // INITIAL LOAD
            if (!IsPostBack) // checks if the page is being loaded for the first time
            {
                Number_of_Players = (int)Session["PlayerCount"];

                // YOU MAY HAVE TO MANUALLY CLEAR YOUR BROWSERS CACHE SO THE IMAGES LOAD WITH THE CORRECT STYLES
                // make it so no data is cached. otherwise if we try to change the size of the image, the web browser will try to use the cached value instead of the new c sharp value
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetNoStore();
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.Cache.SetExpires(DateTime.Now.AddYears(-1));

                Session["StartTime"] = DateTime.Now; // keep track of how long the player has survived
                Session["PointsCollected"] = pointsCollected; // store points collected as a session variable

                // fill the table with the appropriate number of rows and columns based on the constants instantiated
                Init_Table();

                // assign the appropriate height, width, and other values to the CssClasses that the images derive from so they can be properly sized and placed to their containing table cells
                Update_CSS_Files();

                // asign the images for each of the player segments
                playerHeadImageFormat.ImageUrl = "~/Snathon/Images/Nathan Head.png"; //+ "?" + Session["StartTime"]; // add ?###### to the end of a file to make it uncachable without changing the file. using the current time as the number ensures the number will never repeat
                playerHeadImageFormat.CssClass = "GameElementImage PlayerImageStyle";
                playerBodyImageFormat.ImageUrl = "~/Snathon/Images/Cole Stan.png"; //+ "?" + Session["StartTime"]; // add ?###### to the end of a file to make it uncachable without changing the file. using the current time as the number ensures the number will never repeat
                playerBodyImageFormat.CssClass = "GameElementImage ItemImageStyle";

                // create players
                CreatePlayers();

                // asign the images for each item
                itemImageFormat.ImageUrl = "~/Snathon/Images/Heart.png";
                itemImageFormat.CssClass = "GameElementImage";
                Session["itemImageFormat"] = itemImageFormat;

                // add initial item to the screen
                // add the number of items as there are players
                //for (int i = 0; i < Number_of_Players; i++)
                //{
                    AddItem();
                //}
            }
            else
            {
                // EVERY LOAD
                // Retrieve the needed unloaded objects from Session state
                RetrieveSessionVariablesToThisPostback();

                ApplyPlayerInputs();

                // readd all game elements to the screen because they are deleted on reload of the page
                RefreshElements();
     
                if (CountAlivePlayers() > 0) // when at least one player is alive
                {
                    // handle collisions if they occur
                    HandlePossibleCollisions();
                }
            }
        }
        private void RetrieveSessionVariablesToThisPostback()
        {
            players = (List<Player>)Session["Players"];

            items = (List<Item>)Session["Items"];
            itemImageFormat = (Image)Session["itemImageFormat"];

            pointsCollected = (int)Session["PointsCollected"];
        }
        private void ApplyPlayerInputs()
        {
            // add all hidden fields storing player inputs to a list
            List<string> playerInputs = new List<string>();
            playerInputs.Add(keyPressedPlayer1.Value);
            playerInputs.Add(keyPressedPlayer2.Value);
            playerInputs.Add(keyPressedPlayer3.Value);
            playerInputs.Add(keyPressedPlayer4.Value);

            for (int i = 0; i < players.Count; i++) // for each player
            {
                if (players[i].IsAlive) // only move the player while its alive
                {
                    // Convert keys pressed into a movement direction
                    Player.MovementDirection movementDirection = GetMovementDirectionFromKeyStroke(playerInputs[i], players[i]);

                    // move the snake and make sure it is in bounds of the table
                    if (!players[i].tryMoveSnake(movementDirection)) // if false, the snake is out of bounds of the table
                    {
                        // begin player death sequence for out of bounds player
                        BeginPlayerDestruction(players[i]);
                    }
                }
                else // player is dead
                {
                    // one by one with every refresh remove player segments from the screen start from the tail towards the head
                    players[i].TryRemoveSegment();
                } // when collision the head stops, but the body moves forward one more ---------------------------------------------------------------
                
            }
        }
        private Player.MovementDirection GetMovementDirectionFromKeyStroke(string keyCode, Player player)
        {
            // Convert key pressed into a movement direction
            Player.MovementDirection movementDirection;
            if (keyCode == "87" || keyCode == "104" || keyCode == "73" || keyCode == "38") // keyCode for w 8 i up_arrow
            {
                movementDirection = Player.MovementDirection.Up;
            }
            else if (keyCode == "83" || keyCode == "101" || keyCode == "75" || keyCode == "40") // keyCode for s 5 k down_arrow
            {
                movementDirection = Player.MovementDirection.Down;
            }
            else if (keyCode == "65" || keyCode == "100" || keyCode == "74" || keyCode == "37") // keyCode for a 4 j left_arrow
            {
                movementDirection = Player.MovementDirection.Left;
            }
            else if (keyCode == "68" || keyCode == "102" || keyCode == "76" || keyCode == "39") // keyCode for d 6 l right_arrow
            {
                movementDirection = Player.MovementDirection.Right;
            }
            else // move in the last moved direction
            {
                movementDirection = player.Head.RecentMovement;
            }

            return movementDirection;
        }
        private int CountAlivePlayers()
        {
            int numAlivePlayers = 0; // keep track of the number of alive players
            foreach (Player player in players) // loop through each player
            {
                // increment our counter when the current player is alive
                if (player.IsAlive)
                    numAlivePlayers++;
            }

            return numAlivePlayers;
        }
        private void HandlePossibleCollisions()
        {
            // Check if a collision has occurred
            Dictionary<Player, System.Drawing.Point> collisions = GetCollisionPointsAndCollidedPlayer();
            if (collisions.Count > 0) // a collision occurred
            {
                // get each collisions type
                foreach (KeyValuePair<Player, System.Drawing.Point> collisionData in collisions)
                {
                    // get data from key value pair
                    Player player = collisionData.Key;
                    System.Drawing.Point collisionPoint = collisionData.Value;

                    // get the cell of the collision
                    int row = collisionPoint.X;
                    int col = collisionPoint.Y;
                    TableCell collisionCell = tGameArea.Rows[row].Cells[col];

                    // store collided Player Segments (stored as images on the table)
                    List<Image> collidedSegments = new List<Image>();
                    // store collided Items (stored as images on the table)
                    List<Image> collidedItems = new List<Image>();

                    // count the number of collided player segments and items
                    foreach (Image img in collisionCell.Controls) // gameObjects are images on the table
                    {
                        if (img.ImageUrl == "~/Snathon/Images/Cole Stan.png") // player head segment
                        {
                            collidedSegments.Add(img);
                        }
                        if (img.ImageUrl == "~/Snathon/Images/Nathan Head.png") // player body segment
                        {
                            collidedSegments.Add(img);
                        }
                        if (img.ImageUrl == "~/Snathon/Images/Heart.png") // item
                        {
                            collidedItems.Add(img);
                        }
                    }

                    // Player/Player Collision
                    if (collidedSegments.Count > 1)
                    {
                        // begin player death sequence for player who collided
                        BeginPlayerDestruction(player);
                    }
                    // Player/Item Collision
                    else if (collidedItems.Count > 0)
                    {
                        // add a new segment to the player
                        player.addSegment();
                        // update the session variable for player
                        //Session["Players"] = player;

                        /* OPTION */
                        // remove item in specified cell (if you only want the collided item removed)
                        //RemoveItemFromTablePoint(new System.Drawing.Point(row, col));
                        // remove all items (if you want to have a new set of items spawn when an item is destroyed)
                        items.Clear();
                        /* OPTION END */

                        // increments points collected
                        pointsCollected++;
                        // update the session variable for points collected
                        Session["PointsCollected"] = pointsCollected;

                        // reload only the points scored label 
                        upScoreBoard.Update();
                        //ScriptManager.RegisterStartupScript(this, GetType(), "PostBack", $"__doPostBack('{lPointsScored}', '')", true);

                        /* OPTION */
                        // add a new item to the table
                        //AddItem();
                        AddItems(20);
                        /* OPTION END */
                    }
                }
            }
        }
        private void BeginPlayerDestruction(Player player)
        {
            // tell the player its dead
            player.IsAlive = false;
            // make the whole player transparent
            player.MakeAllPlayerSegmentsTransparent();

            //players.Remove(players[i]); // remove the player from the list of alive players; however, it should stay in the playerData list so the gamne can still interact with it
            //Session["Players"] = players; // save alive players

            // this stop the player from being able to move which is implement elsewhere in ApplyPlayerInputs
        }
        private void RefreshElements()
        {
            // refresh table
            Init_Table();

            // refresh players
            Init_Players();

            // refresh items
            Init_Items();
        }
        private void CreatePlayers()
        {
            // set the proper spacing for our list of empty cells
            int minDistanceFromTableEdge = 3;
            int minDistanceBetweenOccupiedCells = 4;
            // Get a list of empty cells with the proper spacing
            List<System.Drawing.Point> emptyCellCoordinates = GenerateCellsWithSpacing(minDistanceFromTableEdge, minDistanceBetweenOccupiedCells, tGameArea);

            // store the coordinate of the previous cell filled with a player
            System.Drawing.Point previouslyFilledCellCoordinate = new System.Drawing.Point(-1, -1); // instantiated to (-1, -1) because there will be no previously filled cell for the first player (needs to be instantiated to something to get rid of a severe warning)

            // run this procedure for the number of times there are players
            for (int i = 0; i < Number_of_Players; i++)
            {
                if (i != 0) // every player other than the first player will have a previously filled cell
                {
                    emptyCellCoordinates = ReevaluateValidRangeOfEmptyCells(previouslyFilledCellCoordinate, emptyCellCoordinates, minDistanceFromTableEdge, minDistanceBetweenOccupiedCells, tGameArea);
                }

                System.Drawing.Point randomEmptyCellCoordinate = PickRandomCoordinateFromCoordinates(emptyCellCoordinates);
                previouslyFilledCellCoordinate = randomEmptyCellCoordinate; // save the coordinate
                int row = randomEmptyCellCoordinate.X;
                int col = randomEmptyCellCoordinate.Y;
                // get the cell
                TableCell cell = tGameArea.Rows[row].Cells[col];

                // create player and add it to the player list
                players.Add(new Player(playerHeadImageFormat, playerBodyImageFormat, new System.Drawing.Point(row, col), tGameArea));

                // put the player in the randomly selected cell
                cell.Controls.Add(players[i].Head.ObjectImage);

                // store a copy of player data in alive players
                players = Copy_Players_List(players);
            }

            // Store the players in Session state
            Session["Players"] = players;
        }
        private List<System.Drawing.Point> ReevaluateValidRangeOfEmptyCells(System.Drawing.Point fullCellCoordinate, List<System.Drawing.Point> suspectedValidCells, int minDistanceFromTableEdge, int minDistanceBetweenOccupiedCells, Table table)
        {
            // Re evalulate the cells in range off the cell that was filled according to the rules for space between the table edge and full cells given
            int row = fullCellCoordinate.X;
            int col = fullCellCoordinate.Y;

            // set the starting row to the row that leaves the given distance to the left of the current row;
            // this value will be 0 (the table's first row) if the min distance brings the loop outside the table
            int leftBoundRow = Math.Max(row - minDistanceBetweenOccupiedCells, 0);
            // set the ending row to the row that leaves the given distance to the right of the current row;
            // this value will be the last row of the table if the min distance brings the loop outside the table
            int rightBoundRow = Math.Min(row + minDistanceBetweenOccupiedCells, table.Rows.Count - 1);
            // set the starting column to the column that leaves the given distance above of the current column;
            // this value will be 0 (the table's first column) if the min distance brings the loop outside the table
            int upperBoundCol = Math.Max(col - minDistanceBetweenOccupiedCells, 0);
            // set the ending column to the column that leaves the given distance below the current column;
            // this value will be the last column of the table if the min distance brings the loop outside the table
            int lowerBoundCol = Math.Min(col + minDistanceBetweenOccupiedCells, table.Rows[0].Cells.Count - 1);

            // remove each cell from our suspectedValidCells list that is in range of the filled cell
            for (int r = leftBoundRow; r <= rightBoundRow; r++)
            {
                for (int c = upperBoundCol; c <= lowerBoundCol; c++)
                {
                    // if the current cell contains any children, this cell is not valid so break
                    suspectedValidCells.Remove(new System.Drawing.Point(r, c));
                }
            }

            // return our final list
            return suspectedValidCells;
        }
        private List<System.Drawing.Point> GenerateCellsWithSpacing(int minDistanceFromTableEdge, int minDistanceBetweenOccupiedCells, Table table)
        {
            // store a list of empty cells
            List<System.Drawing.Point> emptyCellCoordinates = new List<System.Drawing.Point>();

            // loop through every cell in the given table and the distance from edge and occupied cells
            for (int row = minDistanceFromTableEdge; row < table.Rows.Count - minDistanceFromTableEdge; row++)
            {
                // store the current row
                TableRow tableRow = table.Rows[row];

                for (int col = minDistanceFromTableEdge; col < table.Rows[0].Cells.Count - minDistanceFromTableEdge; col++)
                {
                    // is the cell empty
                    if (tableRow.Cells[col].Controls.Count == 0)
                    {
                        // set the starting row to the row that leaves the given distance to the left of the current row;
                        // this value will be 0 (the table's first row) if the min distance brings the loop outside the table
                        int leftBoundRow = Math.Max(row - minDistanceBetweenOccupiedCells, 0);
                        // set the ending row to the row that leaves the given distance to the right of the current row;
                        // this value will be the last row of the table if the min distance brings the loop outside the table
                        int rightBoundRow = Math.Min(row + minDistanceBetweenOccupiedCells, table.Rows.Count - 1);
                        // set the starting column to the column that leaves the given distance above of the current column;
                        // this value will be 0 (the table's first column) if the min distance brings the loop outside the table
                        int upperBoundCol = Math.Max(col - minDistanceBetweenOccupiedCells, 0);
                        // set the ending column to the column that leaves the given distance below the current column;
                        // this value will be the last column of the table if the min distance brings the loop outside the table
                        int lowerBoundCol = Math.Min(col + minDistanceBetweenOccupiedCells, tableRow.Cells.Count - 1);

                        // Check if the cell is at least ___ cells away from any cell with children
                        for (int r = leftBoundRow; r <= rightBoundRow; r++)
                        {
                            TableRow currentRow = table.Rows[r]; // store the current row
                            for (int c = upperBoundCol; c <= lowerBoundCol; c++)
                            {
                                // if the current cell contains any children, this cell is not valid so break
                                if (currentRow.Cells[c].Controls.Count > 0)
                                    break;
                            }
                        }

                        // if we reach this point, the chosen cell has the correct spacing, add it to the list
                        emptyCellCoordinates.Add(new System.Drawing.Point(row, col));
                    }
                }
            }

            //// DEBUG - put a cole in every empty cell
            //foreach (System.Drawing.Point emptyCell in emptyCellCoordinates)
            //{
            //    int row = emptyCell.X;
            //    int col = emptyCell.Y;

            //    Image img = new Image();
            //    img.ImageUrl = playerBodyImageFormat.ImageUrl;
            //    img.CssClass = playerBodyImageFormat.CssClass;

            //    tGameArea.Rows[row].Cells[col].Controls.Add(img);

            //}

            return emptyCellCoordinates;
        }
        private System.Drawing.Point PickRandomCoordinateFromCoordinates(List<System.Drawing.Point> coordinates)
        {
            // Choose a random cell coordinate
            Random random = new Random();
            int randomIndex = random.Next(coordinates.Count);
            System.Drawing.Point cellCoordinate = coordinates[randomIndex];
            //// Get the cell from the coordinate
            //int row = cellCoordinate.X;
            //int col = cellCoordinate.Y;
            //TableCell cell = tGameArea.Rows[row].Cells[col];

            return cellCoordinate;
        }
        private void AddItem()
        {
            // Create a list of empty cell coordinates
            List<System.Drawing.Point> emptyCellCoordinates = GetEmptyCellCoordinates(tGameArea);
            // Choose a random cell coordinate
            Random random = new Random();
            int randomIndex = random.Next(emptyCellCoordinates.Count);
            System.Drawing.Point cellCoordinate = emptyCellCoordinates[randomIndex];
            // Get the cell from the coordinate
            int row = cellCoordinate.X;
            int col = cellCoordinate.Y;
            TableCell cell = tGameArea.Rows[row].Cells[col];

            // create an item
            Item item = new Item(itemImageFormat, cellCoordinate);
            // Put an item in the randomly selected cell ADD IT TO ITEMS AND IT WILL BE ADDED TO TABLE ON REFRESH SO DONT NEED TO ADD IT HERE
            cell.Controls.Add(item.ObjectImage);

            // Store the items list in Session state
            items.Add(item);
            Session["Items"] = items;
        }
        private void AddItems(int numItems)
        {
            // Create a list of empty cell coordinates
            List<System.Drawing.Point> emptyCellCoordinates = GetEmptyCellCoordinates(tGameArea);
            // create a random number generator
            Random random = new Random();

            // create the given number of items
            for (int i = 0; i < numItems; i++)
            {
                // Choose a random cell coordinate
                int randomIndex = random.Next(emptyCellCoordinates.Count); // the number of emptyCellCoordinates decrements with each item added
                System.Drawing.Point cellCoordinate = emptyCellCoordinates[randomIndex];

                emptyCellCoordinates.RemoveAt(randomIndex); // remove the cell at the randomly chosen index so it cannot be chosen again

                // Get the cell from the coordinate
                int row = cellCoordinate.X;
                int col = cellCoordinate.Y;
                TableCell cell = tGameArea.Rows[row].Cells[col];

                // create an item
                Item item = new Item(itemImageFormat, cellCoordinate);
                // Put an item in the randomly selected cell ADD IT TO ITEMS AND IT WILL BE ADDED TO TABLE ON REFRESH SO DONT NEED TO ADD IT HERE
                cell.Controls.Add(item.ObjectImage);

                // Store the items list in Session state
                items.Add(item);
            }
            // save the items to be loaded in during the next postback
            Session["Items"] = items;
        }
        private void RemoveItemFromTablePoint(System.Drawing.Point point)
        {
            // find the item in the items list that shares a location with our collided item. (There can only be one item in every cell)
            Item itemToRemove = new Item(itemImageFormat, new System.Drawing.Point(-1, -1)); // store the collided item when found (default as new item)
            foreach (Item item in items)
            {
                // if the current items location is the same as our collided items
                if (item.ContainingCellCoordinate.X == point.X && item.ContainingCellCoordinate.Y == point.Y)
                {
                    // save the collided item (cannot be removed will enumerating over the list)
                    itemToRemove = item;
                    // we found our item, so get out of the loop
                    break;
                }
            }
            // remove item
            items.Remove(itemToRemove);
        }
        private List<System.Drawing.Point> GetEmptyCellCoordinates(Table table)
        {
            // store a list of empty cells
            List<System.Drawing.Point> emptyCellCoordinates = new List<System.Drawing.Point>();

            // loop through every cell in the given table
            for (int row = 0; row < table.Rows.Count; row++)
            {
                // store the current row
                TableRow tableRow = table.Rows[row];
                for (int col = 0; col < table.Rows[0].Cells.Count; col++)
                {
                    // add empty cells to the list
                    if (tableRow.Cells[col].Controls.Count == 0)
                    {
                        emptyCellCoordinates.Add(new System.Drawing.Point(row, col));
                    }
                }
            }
            return emptyCellCoordinates;
        }
        private Dictionary<Player, System.Drawing.Point> GetCollisionPointsAndCollidedPlayer()
        {
            // Store a dictionary of the point where each player collides if they collide at all
            Dictionary<Player, System.Drawing.Point> collisionPoints = new Dictionary<Player, System.Drawing.Point>();

            // get each alive player's head's containing cell (the head is the only object that should be able to cause a collision)
            foreach (Player player in players)
            {
                if (player.IsAlive) // only alive players can make a collision
                {
                    int row = player.Head.ContainingCellCoordinate.X;
                    int col = player.Head.ContainingCellCoordinate.Y;
                    TableCell currentCell = tGameArea.Rows[row].Cells[col];

                    // if there is more than one GameObject in a single sell
                    if (currentCell.Controls.Count > 1)
                    {
                        // a collision has occurred
                        collisionPoints.Add(player, new System.Drawing.Point(row, col)); // add the collision's location and the player that collided
                    }
                }
            }

            //return the list of collision; the list will be empty if no collisions occurred
            return collisionPoints;
        }
        private void Init_Table()
        {
            // resize columns so they fit in the table's dimensions
            ROW_HEIGHT = (double)TABLE_HEIGHT / NUMBER_OF_TABLE_ROWS;
            COLUMN_WIDTH = (double)TABLE_WIDTH / NUMBER_OF_TABLE_COLUMNS;
            Session["ROW_HEIGHT"] = ROW_HEIGHT;
            // create table
            for (int r = 0; r < NUMBER_OF_TABLE_ROWS; r++)
            {
                // create row and apply dimensions
                TableRow row = new TableRow();
                row.Style["height"] = $"{ROW_HEIGHT}px";
                for (int c = 0; c < NUMBER_OF_TABLE_COLUMNS; c++)
                {
                    // create cell and apply dimensions
                    TableCell cell = new TableCell();
                    cell.Style["width"] = $"{COLUMN_WIDTH}px";

                    // add the cell to the row
                    row.Cells.Add(cell);
                }
                // add the row to the table
                tGameArea.Rows.Add(row);
            }
        }
        private void Init_Players()
        {
            foreach (Player player in players)
            {
                // refresh each segment individually, starting from the tail
                PlayerSegment currentSegment = player.Tail;
                for (int i = 0; i < player.Length; i++)
                {
                    int row = currentSegment.ContainingCellCoordinate.X;
                    int col = currentSegment.ContainingCellCoordinate.Y;
                    // add the element to its cell
                    tGameArea.Rows[row].Cells[col].Controls.Add(currentSegment.ObjectImage);

                    // get the next segment
                    currentSegment = currentSegment.MasterSegment;
                }
            }
        }
        private void Init_Items()
        {
            // refresh each item
            foreach (Item item in items)
            {
                int row = item.ContainingCellCoordinate.X;
                int col = item.ContainingCellCoordinate.Y;
                // add the element to its cell
                tGameArea.Rows[row].Cells[col].Controls.Add(item.ObjectImage);
            }
        }
        private void Update_CSS_Files()
        {
            // assign the appropriate height, width, and other values to the CssClasses that the images derive from so they can be properly sized and placed to their containing table cells

            // Specify the path to the CSS file
            string cssFilePath = "~/Snathon/styles.css";

            // Read the contents of the CSS file
            string cssContent = File.ReadAllText(Server.MapPath(cssFilePath));

            // replace only the height and width rules inside the css class
            cssContent = Regex.Replace(cssContent, @"\.GameElementImage\s*{([^}]*(height|width)[^}]*)}", $".GameElementImage {{ \n\theight: {ROW_HEIGHT}px; \n\twidth: {COLUMN_WIDTH}px; \n}}");
            // replace all rules in the css class
            //cssContent = Regex.Replace(cssContent, @"\.GameElementImage\s*{([^}]*)}", ".GameElementImage { height: 75px; width: 75px; }");
            // add the new rules to the css class, will not be removed at upon closing the website
            //cssContent = cssContent.Replace(".GameElementImage {", ".GameElementImage {\nheight: 75px;\nwidth: 75px;");

            // adjust the transform that the images inside table cells are shifted according to their new size
            cssContent = Regex.Replace(cssContent, @"(?<=td\s*>\s*img\s*\{[^{}]*\btransform\s*:\s*translate\(0,\s*-?)[\d.]+(?=px\s*\))", $"{ROW_HEIGHT/2}");

            // Write the modified contents back to the file
            File.WriteAllText(Server.MapPath(cssFilePath), cssContent);
        }
        private List<Player> Copy_Players_List(List<Player> playersList)
        {
            // create new list of players
            List<Player> newPlayersList = new List<Player>();
            // put each player from list into new list
            foreach (Player player in playersList)
            {
                newPlayersList.Add(player);
            }

            return newPlayersList;
        }

        private void LoadEndGamePage()
        {
            // load new page with stats etc
        }
    }
}
//Daud Jusab
//0671537
//Cois 3020
//Subway maps assignment 1


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Colour { RED, YELLOW, GREEN } //colours used for the subway lines 
class Node
{
    public Station Connection { get; set; } // Adjacent station (connection)
    public Colour Line { get; set; } // Colour of its subway line
    public Node Next { get; set; } // Link to the next adjacent station (Node)
    public Node(Station connection, Colour c, Node next)
    {
        Connection = connection;
        Line = c;
        Next = next;
    }
    
}
class Station
{
    public string Name { get; set; } // Name of the subway station

    public bool Visited { get; set; } // Used for depth-first and breadth-first searches

    public Node E { get; set; } // Linked list of adjacent stations

    public Station(string name)  // Station constructor 
    {
        Name = name;
        Visited = false;   //setting visited to false
        E = new Node(null, default, null);  //initializing the first node in the linked list to null
    } 
}
class SubwayMap
{
    private List<Station> S; // List of stations
    public SubwayMap()   // SubwayMap constructor 
    {
        S = new List<Station>();  //initilizing the S as the list of stations
    }

    //Method: FindStation
    //Parameters: Takes the string name
    //Return type: integer
    //Description: This method is used to check if the name of the station passed exists in the list of stations S.
    public int FindStation(string name)
    {
        int i;

        for (i = 0; i < S.Count; i++)  //for loop to go through each index available in the list
        {
            if (S[i].Name.Equals(name))  //checks if the name at the index is equal to the name of the station passed
                return i;   // returns the index of the station if found
        }
        return -1;  //if the station is not found then returns -1
    }

    //Method: displayStations
    //Parameters: No parameters
    //Description: This method is used to display all the stations currently available in the list S.
    public void displayStations()
    {
        if (S.Count <= 0)  // Checks if the list S does not contain anything
        {
            Console.WriteLine("There is no such station in the subway!\n");
        }
        else  //If the list S contains stations
        {
            for (int i = 0; i < S.Count; i++)  //iterates through all the index that contain stations
                Console.WriteLine(S[i].Name);  //Prints the name of the stations
        }
    }

    //Method: PrintConnection
    //Parameters: No parameters
    //Description: This method is used to print the edge between two adjacent stations along with the colour of the line
    public void PrintConnection()
    {
        int i;
        Node curr;
        bool found = false;

        if (S.Count == 0 || S.Count == 1)   //when there is no or only one station in the subway map
        {
            Console.WriteLine("There is no any connection yet");
        }
        for (i = 0; i < S.Count; i++)  //iterates through all the index that contain stations
        {
            curr = S[i].E;
            while (curr.Next != null)  //while the next node storing the adjacent station is not equal to null
            {
                Console.WriteLine("[(" + S[i].Name + ") to (" + curr.Next.Connection.Name + ") " + curr.Next.Line + "]");
                found = true;
                curr = curr.Next;  //move to the next node storing the adjacent station
            }
        }
        if (!found)  //checks if there was no any connection found
        {
            Console.WriteLine("There is no any connection yet");
        }
    }

    //Method: InsertStation
    //Parameters: string name
    //Description: This method is used to insert a station into the list of subway stations S 
    public void InsertStation(string name)  // Duplicate station names are not allowed 
    {
        if (FindStation(name) == -1)   //checks if the station is in the list S
        {
            Station A = new Station(name);   //initializes the station
            S.Add(A);   //Adds the station into the list S
            Console.WriteLine("Added the station succesfully");
        }
        else  //If the station exists 
        {
            Console.WriteLine("The station already exists");
        }
    }

    //Method: RemoveStation
    //Parameters: string name
    //Description: This method is used to remove a station from the list of subway stations S
    public void RemoveStation(string name)
    {
        int i;
        Node curr;
        if ((i = FindStation(name)) == -1)   //checks if the method findstation returns -1
        {
            Console.WriteLine("This station does not exist");
            return;
        }
        else   //goes through if the findstation does not return -1
        {
            for (i = 0; i < S.Count(); i++)   //iterates through all the stations in the list S
            {
                curr = S[i].E;  //curr is equal to the first adjacent node

                while (curr.Next != null)   //while the next adjacent node is not equal to null
                {
                    if (curr.Next.Connection.Name == name)  //if the name of the next adjacent station is equal to the station being removed
                    {
                        curr.Next = curr.Next.Next; //removes the node containing the station
                    }
                    else
                    {
                        curr = curr.Next;  //moves to the next node
                    }

                }

            }
            i = FindStation(name);  //get the index where the the station is stored in the list S
            S.RemoveAt(i);  //removes the station from list S
            Console.WriteLine("Removed the station successfully");
        }
    }

    //Method: InsertConnection
    //Parameters: string name1, string name2, Colour c
    //Description: This method is used to insert a new connection between two stations using the line(colour)
    public void InsertConnection(string name1, string name2, Colour c)
    {
        int i, j;

        // Do the stations exist?
        if ((i = FindStation(name1)) > -1 && (j = FindStation(name2)) > -1)
        {
            Node curr = S[i].E;
            Node Now = S[j].E;

            if (curr.Next == null)  //used to enter the first connection for the station stored at index i
            {
                curr.Next = new Node(S[j], c, null);
            }
            else  //Goes through if the station at index i has atleast one connection 
            {
                while (curr.Next != null)  //loop to check if the next node is null 
                {
                    if (curr.Next.Connection.Name == name2)  //if the name of the station in the next node is the same as name2
                    {
                        if (curr.Next.Line == c)  //checking if the colours match
                        {
                            Console.WriteLine("This connection already exists");
                            return;
                        }
                        else  //goes through if the colours do not match
                        {
                            curr = curr.Next;  //moves to the next node
                        }
                    }
                    else
                        curr = curr.Next;
                }
                curr.Next = new Node(S[j], c, null);  //creates the new adjacent station  

            }

            //Creating the new node for the adjacent station since it's undirected
            while (Now.Next != null)
            {
                Now = Now.Next;
            }
            Now.Next = new Node(S[i], c, null);
            Console.WriteLine("inserted the connection succesfully");
        }
        else
        {
            Console.WriteLine("one or both of the stations do not exist");
        }
    }

    //Method: RemoveConnection
    //Parameters: string name1, string name2, Colour c
    //Description: This method is used to remove the connection between two adjacent stations
    public void RemoveConnection(string name1, string name2, Colour c)
    {
        int i, j;
        // Do the stations exist?
        if ((i = FindStation(name1)) > -1 && (j = FindStation(name2)) > -1)
        {
            Node curr = S[i].E;
            Node Now = S[j].E;

            if (curr.Next == null) //if the station does not have any connection
            {
                Console.WriteLine("There is no such connection");
                return;
            }
            else
            {
                while (curr.Next != null)  //loop to go through all the the adjacent nodes
                {
                    if (curr.Next.Connection.Name == name2)  //if the name of the adjacent station is equal to name2
                    {
                        if (curr.Next.Line == c)   //checking if the colours match
                        {
                            curr.Next = curr.Next.Next;  //removes the node containing the adjacent station

                            while (Now.Next != null)
                            {
                                if (Now.Next.Connection.Name == name1)  //if the name of the station is equal to name2
                                {
                                    if (Now.Next.Line == c)   //checking if the colours match
                                    {
                                        Now.Next = Now.Next.Next; //removes the node containing the station
                                        Console.WriteLine("Removed the connection successfully");
                                        return;
                                    }
                                    else
                                    {
                                        Now = Now.Next;  //moves to the next node
                                    }
                                }
                                else
                                {
                                    Now = Now.Next;  //moves to the next node
                                }
                            }
                        }
                        else
                        {
                            curr = curr.Next;  //moves to the next node
                        }
                    }
                    else
                    {
                        curr = curr.Next;  //moves to the next node
                    }

                }
                Console.WriteLine("There is no such a connection");
            }
        }
        else
        {
            Console.WriteLine("one or both the stations do not exist");
        }
    }

    //Method: RemoveConnection
    //Parameters: string from, string to
    //Description: This method is used to display the fastest path from one station to another
    public void FastestRoute(string from, string to)
    {
        //data variables
        int i, j;
        Station[] parent;
        parent = new Station[S.Count];
        List<Station> path = new List<Station>();

        for (i = 0; i < parent.Length; i++)   //for loop to make all indeces in the array to null
            parent[i] = null;

        for (i = 0; i < S.Count; i++)  //iterating through all the stations in the list S
        {
            S[i].Visited = false;   //marking each station as unvisited

            if (S[i].E.Connection != null)  //checks if there is an adjacent node
                S[i].E.Connection.Visited = false;  //marks the adjacent nodes as unvisited
        }

        if ((i = FindStation(from)) > -1 && (j = FindStation(to)) > -1)  //checks if the to and from stations exist
        {
            Queue<Station> Q = new Queue<Station>();  //creating a new queue
            Station A = S[i];   //A is the starting position
            A.Visited = true;   //marks the from station as visited
            Q.Enqueue(A);    //puts the station A into the queue

            while (Q.Count != 0)  //while the queue is not empty
            {
                A = Q.Dequeue();   //removes the station at the front of the queue and stores it into A

                Node curr = A.E;   //curr pointing at the linked list of adjacent stations

                while (curr.Next != null)  //while an adjacent station exists
                {
                    if (curr.Next.Connection.Visited == false)  //checks if the station is not visited
                    {
                        Q.Enqueue(curr.Next.Connection);   //pushes the adjacent station into the queue
                        curr.Next.Connection.Visited = true;  //marks the adjacent station as visited
                        parent[FindStation(curr.Next.Connection.Name)] = A;   //stores the parent for the adjacent nodes
                    }

                    if (curr.Next.Connection.Name == to)   //checks if the destination is found
                    {
                        path.Add(curr.Next.Connection);  //adds the destination to the path
                        A = parent[FindStation(curr.Next.Connection.Name)]; //stores the parent for the next adjacent node
                        while (A.Name != from)  //while the from station is not reached yet
                        {
                            path.Add(A);  //adds the parent nodes
                            A = parent[FindStation(A.Name)];
                        }
                        path.Add(A); //adds the from station into the list 

                        for (j = path.Count; j != 1; j--)   //iterates through the list of the path 
                        {
                            A = path[j - 1];  //stores the from station in A
                            curr = A.E;  //stores the adjacent nodes of A as curr
                            while (curr.Connection != path[j - 2])  //while loop for the purpose of getting the colour
                            {
                                curr = curr.Next;  //moves to the next node
                            }
                            Console.Write(" " + path[j - 1].Name + " " + "[" + curr.Line + "]" + "->");
                        }
                        Console.WriteLine(" " + path[j - 1].Name);
                        return;
                    }
                    curr = curr.Next;  //moves to the next node
                }

            }
            Console.WriteLine("There is no any path to the destination");
        }
        else
            Console.WriteLine("One or both the stations do not exist");
    }

    public void CriticalConnections()
    {
        int i;
        int[] lowTime = new int[S.Count];   //helps to know the artculation points
        int[] discoveryTime = new int[S.Count];   //keeps the order of when the nodes are visited
        int timer = 0;
        List<Station> CriticalConnection = new List<Station>();

        for (i = 0; i < S.Count; i++)   //loop to marks all stations as unvisited
            S[i].Visited = false;  
       

        DepthFirstSearch(S[0], null, CriticalConnection, lowTime, discoveryTime, timer);  //calling the dfs for the first time

        if(CriticalConnection.Count==0)   //checks if the list for the crirical connection is empty
        {
            Console.WriteLine("There is no any critical connection");
        }

        Console.WriteLine("The critical connections are between the following stations,");

        for(i=0; i < CriticalConnection.Count; i=i+2)  //for loop for displaying the critical connections
        {
            Console.WriteLine(CriticalConnection[i].Name + " and " + CriticalConnection[i + 1].Name);
        }
    }



    public void DepthFirstSearch(Station A, Station parent, List<Station> CriticalConnection, int[] lowTime, int[] discoveryTime, int timer)
    {


        A.Visited = true;  //marks A as visited

        discoveryTime[FindStation(A.Name)] = lowTime[FindStation(A.Name)] = timer++;   //discoverytime and the lowtime of the station equal the timer 

        Station B;

        Node curr = A.E;  //curr points to the adjacent nodes

        while (curr.Next != null)   //while the adjacent station is not empty
        {
            B = curr.Next.Connection;  //B is the adjacent station
            if (B == parent)   //checks if B is a parent then there is no need to check it again
            {
                curr = curr.Next;  //goes to the next adjacent node
            }
            else  //if B is not a parent
            {
                if (!B.Visited)  //checks if the adjacent station is not visited
                {
                    DepthFirstSearch(B, A, CriticalConnection, lowTime, discoveryTime, timer);   //recursively calls the dfs method
                    lowTime[FindStation(A.Name)] = Math.Min(lowTime[FindStation(A.Name)], lowTime[FindStation(B.Name)]);  //lowtime of the station A will be the minimum of the lowtime of A or B 

                    if (discoveryTime[FindStation(A.Name)] < lowTime[FindStation(B.Name)])  //checks if the discoverytime of the station is lower than the lowtime of the adjacent station
                    {
                        CriticalConnection.Add(A);  //adds A to the list of critical connections
                        CriticalConnection.Add(B);  //adds B to the list of critical connections
                    }
                    curr = curr.Next;   //goes to the next adjacent node
                }
                else
                {
                    lowTime[FindStation(A.Name)] = Math.Min(lowTime[FindStation(A.Name)], discoveryTime[FindStation(B.Name)]);  //lowtime of A will be the minimum of the lowtime of A or the discoverytime of B 
                    curr = curr.Next;   //goes to the next adjacent node
                }
            }

        }


    }
}
    class program
{
    public static void Main(string[] args)
    {

        {
            SubwayMap sub = new SubwayMap();
            string Start;
            string End;
            int colour;
            bool done = false;
            int command;  // storing the command from the user
            string command1;   //used to check for the incorrect input typr

            Console.Write("Commands you can enter " +
                "\n 1 for Inserting a station, " +
                "\n 2 for Inserting a connection " +
                "\n 3 for Removing a station " +
                "\n 4 for Removing a connection" +
                "\n 5 for finding the fastest route" +
                "\n 6 for finding the critical connection" +
                "\n 7 for Printing all the stations in the subway system" +
                "\n 8 for Printing all the connections " +
                "\n 9 for Quitting the program");                      

            Console.Write("\nPlease enter your command :");       //asking for the user input
            command1 = Console.ReadLine();
            while(!int.TryParse(command1, out command))          //validating the input
            {
                Console.WriteLine("That is an incorrect command");
                Console.Write("\nPlease enter your command :");
                command1 = Console.ReadLine();             //reads the input

            }
            
            command = Convert.ToInt16(command);     //converting the integer to int16 format
            while (command > 9 || command < 1)      //checking if the input is within the range
            {
                Console.Write("Incorrect command.");
                Console.Write("\nPlease enter your command :");
                command1 = Console.ReadLine();
                while (!int.TryParse(command1, out command))    //validating the input
                {
                    Console.WriteLine("That is an incorrect command");
                    Console.Write("\nPlease enter your command :");
                    command1 = Console.ReadLine();         //reads the input

                }

                command = Convert.ToInt16(command);       //converting the integer to int16 format
            }

            while (command != 9)       //goes through as long as the command is not equal to 9
            {


                switch (command)
                {
                    case 1:    //if the input is 1
                        Console.Write("Enter the name of the station : ");
                        sub.InsertStation(Console.ReadLine());   //calling the insertstation method
                        break;

                    case 2:     //if the input is 2
                        done = false;   //initializes done as false
                        Console.Write("Enter the station name where the connection should start : ");    //asking for the starting point
                        Start = Convert.ToString(Console.ReadLine());
                        Console.Write("Enter the station name where the connection should end : ");     //asking for the ending point
                        End = Convert.ToString(Console.ReadLine());
                        while (!done)   //as long as the colour input is not correct
                        {
                            Console.Write("Enter the colour for the connection, \n1 for red \n2 for yellow \n3 for green : ");
                            command1 = Console.ReadLine();
                            while (!int.TryParse(command1, out colour))   //validating the colour input type
                            {
                                Console.WriteLine("That is an incorrect choice");
                                Console.Write("Enter the colour for the connection, \n1 for red \n2 for yellow \n3 for green : ");
                                command1 = Console.ReadLine();
                            }
                            colour = Convert.ToUInt16(colour);    //converting the integer to int16 format

                            if (colour == 1)   //if the colour is red
                            {
                                sub.InsertConnection(Start, End, Colour.RED);    //calling the InsertConnection method
                                done = true;   //marking that the insertion is done
                            }

                            else if (colour == 2)   //if the colour is yellow
                            {
                                sub.InsertConnection(Start, End, Colour.YELLOW);   //calling the InsertConnection method
                                done = true;    //marking that the insertion is done
                            }
                            else if (colour == 3)   //if the colour is green
                            {
                                sub.InsertConnection(Start, End, Colour.GREEN);    //calling the InsertConnection method
                                done = true;     //marking that the insertion is done
                            }
                            else
                                Console.WriteLine("Sorry this colour doesnt exist. Please try again");
                        }
                        break;   //coming out of the case

                    case 3:    //if the input is 3
                        Console.Write("Enter the name of the station : ");
                        sub.RemoveStation(Console.ReadLine());    //calling the removestation method
                        break;

                    case 4:      //if the input is 4
                        done = false;
                        Console.Write("Enter the station name where the connection starts : ");    //asking for the starting point
                        Start = Convert.ToString(Console.ReadLine());
                        Console.Write("Enter the station name where the connection ends : ");     //asking for the ending point
                        End = Convert.ToString(Console.ReadLine()); 

                        while (!done)    //as long as the colour input is not correct
                        {
                            Console.Write("Enter the colour for the connection, \n1 for red \n2 for yellow \n3 for green : ");
                            command1 = Console.ReadLine();
                            while (!int.TryParse(command1, out colour))    //validating the colour input type
                            {
                                Console.WriteLine("That is an incorrect choice");
                                Console.Write("Enter the colour for the connection, \n1 for red \n2 for yellow \n3 for green : ");
                                command1 = Console.ReadLine();
                            }
                            colour = Convert.ToUInt16(colour);

                            if (colour == 1)   //if the colour is red
                            {
                                sub.RemoveConnection(Start, End, Colour.RED);    //calling the RemoveConnection method
                                done = true;
                            }

                            else if (colour == 2)   //if the colour is yellow
                            {
                                sub.RemoveConnection(Start, End, Colour.YELLOW);   //calling the RemoveConnection method
                                done = true;
                            }
                            else if (colour == 3)    //if the colour is green
                            {
                                sub.RemoveConnection(Start, End, Colour.GREEN);    //calling the RemoveConnection method
                                done = true;
                            }
                            else
                                Console.WriteLine("Sorry this colour doesnt exist. Please try again");
                        }
                        break;   //comes of the case 4

                    case 5:     //if the input is 5
                        Console.Write("Enter the station name where the connection starts : ");
                        Start = Convert.ToString(Console.ReadLine());
                        Console.Write("Enter the station name where the connection ends : ");
                        End = Convert.ToString(Console.ReadLine());
                        sub.FastestRoute(Start, End);
                        break;

                    case 6:      //if the input is 6
                        sub.CriticalConnections();    //calling the criticalconnections method
                        break;

                    case 7:      //if the input is 7
                        sub.displayStations();    //calling the displaystations method
                        break;

                    case 8:      //if the input is 8
                        sub.PrintConnection();    //calling the PrintConnection method
                        break;



                }
                Console.Write("Please enter your command :");
                command1 = Console.ReadLine();   
                while (!int.TryParse(command1, out command))    //validating the input type
                {
                    Console.WriteLine("That is an incorrect command");
                    Console.Write("\nPlease enter your command :");
                    command1 = Console.ReadLine();   //reads the input

                }

                command = Convert.ToInt16(command);    //converting the integer to int16 format
                while (command > 9 || command < 1)    //checks if the input is out of range or not
                {
                    Console.Write("Incorrect command.");
                    Console.Write("\nPlease enter your command :");
                    command1 = Console.ReadLine();     //reads the input
                    while (!int.TryParse(command1, out command))    //validating the input type
                    {
                        Console.WriteLine("That is an incorrect command");
                        Console.Write("\nPlease enter your command :");
                        command1 = Console.ReadLine();   //reads the input

                    }
                }

            }

            Console.WriteLine("Thankyou for using this system!");
            Console.ReadKey();

        }

    }
}


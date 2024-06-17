class CafeQueueApp
{
    private static Queue<string> regularQueue = new Queue<string>();

    private static Dictionary<string, string> reservations = new Dictionary<string, string>();

    private static List<string> occupiedTables = new List<string>();

    private const int totalTables = 5;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nCafe Queue Management System");
            Console.WriteLine("1. Add Visitor to Queue");
            Console.WriteLine("2. Add Reservation");
            Console.WriteLine("3. Free a Table");
            Console.WriteLine("4. Display Current Status");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddVisitorToQueue();
                    break;
                case "2":
                    AddReservation();
                    break;
                case "3":
                    FreeTable();
                    break;
                case "4":
                    DisplayStatus();
                    break;
                case "5":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void AddVisitorToQueue()
    {
        Console.Write("Enter visitor name: ");
        string visitorName = Console.ReadLine();

        if (occupiedTables.Count < totalTables)
        {
            occupiedTables.Add(visitorName);
            Console.WriteLine($"{visitorName} got a table immediately.");
        }
        else
        {
            regularQueue.Enqueue(visitorName);
            Console.WriteLine($"{visitorName} is added to the queue.");
        }
    }

    static void AddReservation()
    {
        Console.Write("Enter visitor name: ");
        string visitorName = Console.ReadLine();
        Console.Write("Enter reservation time (HH:mm): ");
        string reservationTime = Console.ReadLine();

        if (!reservations.ContainsKey(reservationTime))
        {
            reservations.Add(reservationTime, visitorName);
            Console.WriteLine($"{visitorName} has reserved a table for {reservationTime}.");
        }
        else
        {
            Console.WriteLine("A reservation already exists for this time.");
        }
    }

    static void FreeTable()
    {
        if (occupiedTables.Count > 0)
        {
            string freedVisitor = occupiedTables[0];
            occupiedTables.RemoveAt(0);
            Console.WriteLine($"{freedVisitor}'s table is now free.");

            string currentTime = DateTime.Now.ToString("HH:mm");
            if (reservations.ContainsKey(currentTime))
            {
                string reservedVisitor = reservations[currentTime];
                occupiedTables.Add(reservedVisitor);
                reservations.Remove(currentTime);
                Console.WriteLine($"{reservedVisitor} took their reserved table.");
            }
            else if (regularQueue.Count > 0)
            {
                string nextVisitor = regularQueue.Dequeue();
                occupiedTables.Add(nextVisitor);
                Console.WriteLine($"{nextVisitor} from the queue got a table.");
            }
        }
        else
        {
            Console.WriteLine("No tables are currently occupied.");
        }
    }

    static void DisplayStatus()
    {
        Console.WriteLine("\nCurrent Occupied Tables:");
        foreach (var visitor in occupiedTables)
        {
            Console.WriteLine(visitor);
        }

        Console.WriteLine("\nCurrent Queue:");
        foreach (var visitor in regularQueue)
        {
            Console.WriteLine(visitor);
        }

        Console.WriteLine("\nReservations:");
        foreach (var reservation in reservations)
        {
            Console.WriteLine($"{reservation.Key}: {reservation.Value}");
        }
    }
}

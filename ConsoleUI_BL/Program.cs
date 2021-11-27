using System;
using IBL.BO;
using BL.BO;
namespace ConsoleUI_BL
{
    class Program
    {
        enum option_a { add = 1, update, display, viewList, exit };
        enum option_b { station = 1, drone,customer,parcel, exit };
            static void Main(string[] args)
        {

            IBL myIBL = new BL();
            
            int mainInput;
            Console.WriteLine("Choose one of the following options:\n" +
                              "for insert options, press 1\n" +
                              "for update options, press 2\n" +
                              "for display options press 3\n" +
                              "For options for viewing the lists, press 4\n" +
                              "To exit, press 5");

            mainInput = int.Parse(Console.ReadLine());
            while (mainInput != 5)
            {
                switch (mainInput)
                {
                    case (int)option.add:
                        addOption();
                        break;
                    case (int)option.update:
                        updateOption();
                        break;
                    case (int)option.display:
                        displayOption();
                        break;
                    case (int)option.viewList:
                        viewListOption();
                        break;
                    case (int)option.exit:
                        Console.WriteLine("BY!!");
                        break;
                    default:
                        Console.WriteLine("ERROR! Please enter a valid value\n");
                        break;
                }
                Console.WriteLine("Choose one of the following options:\n" +
                                  "for insert options, press 1\n" +
                                  "for update options, press 2\n" +
                                  "for display options press 3\n" +
                                  "For options for viewing the lists, press 4\n" +
                                  "To exit, press 5");
                mainInput = int.Parse(Console.ReadLine());
            }
        }

        static void addOption(myIBL)
        {
            try
            {
                int input;
                Console.WriteLine("To add a station, press 1\n" +
                                  "To add a drone press 2\n" +
                                  "To add a new customer press 3\n" +
                                  "for receiving a package for delivery press 4\n" +
                                  "To return to the main menu, press 5");
                input = int.Parse(Console.ReadLine());
                switch (input)
                {

                    case (int)option_b.station:
                      IBL.BO.Station temp = new Station();
                        string nameStation;
                        int idStation, chargeEmpty;
                        double longitudeStation, lattitudeStation;
                        Console.WriteLine("Enter the name of the station");
                        nameStation = Console.ReadLine();
                        Console.WriteLine("Enter the ID of the station");
                        idStation = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the longitude of the station");
                        longitudeStation = float.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the lettitude of the station");
                        lattitudeStation = float.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the empty charge slot of the station");
                        chargeEmpty = int.Parse(Console.ReadLine());
                        temp.name = nameStation;
                        temp.ID = idStation;
                        temp.location = new Location();
                        temp.location.latitude = lattitudeStation;
                        temp.location.longitude = longitudeStation;
                        temp.chargeSlots = chargeEmpty;

                        myIBL.addStations(temp);

                        break;

                    case (int)option_b.drone:
                        Drone myDrone = new Drone();
                        int myId, myModel;
                        double battery;
                        int myWeight;
                        Console.WriteLine("Enter the id of the drone");
                        myId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the model of the drone");
                        myModel = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the weight category of the drone (0 to light, 1 to medium, 2 to heavy)");
                        myWeight = int.Parse(Console.ReadLine());
                        //Console.WriteLine("Enter the drone status ( 0 for available, 1 for maintenace, 2 for delivery)");
                        //myStatus = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the battery percentage");
                        battery = double.Parse(Console.ReadLine());
                        myDrone.ID = myId;
                        myDrone.model = myModel;
                        myDrone.weight = (WeightCategories)myWeight;
                        //myDrone.status = (DroneStatus)myStatus;
                        //myDrone.battery = battery;
                        d.addDrone(myDrone);
                        break;

                    case (int)option_b.customer:
                        Customer women = new Customer();
                        int womenID;
                        string name, phone;
                        float longitude, lattitude;
                        Console.WriteLine("Enter the name of the customer");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter the id of the customer");
                        womenID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the phone of the customer");
                        phone = Console.ReadLine();
                        Console.WriteLine("Enter the longitude");
                        longitude = float.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the lattitude");
                        lattitude = float.Parse(Console.ReadLine());
                        women.ID = womenID;
                        women.name = name;
                        women.phone = phone;
                        women.lattitude = lattitude;
                        women.longitude = longitude;
                        d.addCustomer(women);
                        break;

                    case (int)option_b.parcel:
                        Parcel rut = new Parcel();
                        int id, senderId, targetId, whight, priority; //droneId;
                        DateTime requested, scheduled, pickedUp, delivered;
                        //Console.WriteLine("Enter the id of the percal");
                        id = 0; //int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the  sender id of the percal");
                        senderId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the  target id of the percal");
                        targetId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the weight category of the percal (0 to light, 1 to medium, 2 to heavy)");
                        whight = int.Parse(Console.ReadLine()); ;
                        Console.WriteLine("Enter the prioriyt of the percal (0 for normal, 1 for fast, 2 for emergency)");
                        priority = int.Parse(Console.ReadLine());
                        //Console.WriteLine("Enter the id of the drone");
                        //droneId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the time of the requested at format 2011-03-21");
                        requested = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the time of the scheduled at format 2011-03-21");
                        scheduled = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the time the packge will collect, at format 2011-03-21");
                        pickedUp = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the time the packege delivered, at format 2011-03-21");
                        delivered = DateTime.Parse(Console.ReadLine());
                        rut.ID = id;
                        rut.pickedUp = pickedUp;
                        rut.delivered = delivered;
                        rut.priority = (Priorities)priority;
                        rut.requested = requested;
                        rut.scheduled = scheduled;
                        rut.senderID = senderId;
                        rut.targetId = targetId;
                        rut.weight = (WeightCategories)whight;
                        int newId = myIBL.addParcel(rut);
                        Console.WriteLine("The id of the parcel is " + newId + "\n");
                        break;

                    case (int)option_b.exit: //return to the main menu
                        break;
                    default:
                        Console.WriteLine("ERROR! Please enter a valid value");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void displayOption(myIBL)
        {
            try
            {
                int input;
                Console.WriteLine("for station press 1\n" +
                                  "for drone press 2\n" +
                                  "for customer press 3\n" +
                                  "for percal press 4\n" +
                                  "To return to the main menu, press 5");
                input = int.Parse(Console.ReadLine());
                int id;
                switch (input)
                {
                    case (int)option_b.station:
                        Console.WriteLine("Enter the id of the station");
                        id = int.Parse(Console.ReadLine());
                        Station loky = new Station();
                        loky = myIBL.findStation(id);
                        Console.WriteLine(loky);
                        break;

                    case (int)option_b.drone:
                        Console.WriteLine("Enter the id of the drone");
                        id = int.Parse(Console.ReadLine());
                        Drone flafy = new Drone();
                        flafy = myIBL.findDrone(id);
                        Console.WriteLine(flafy);
                        break;

                    case (int)option_b.customer:
                        Console.WriteLine("Enter the id of the customer");
                        id = int.Parse(Console.ReadLine());
                        Customer anonimy = new Customer();
                        anonimy = myIBL.findCustomer(id);
                        Console.WriteLine(anonimy);
                        break;

                    case 4:
                        Console.WriteLine("Enter the id of the parcel");
                        id = int.Parse(Console.ReadLine());
                        Parcel yoyo = new Parcel();
                        yoyo = myIBL.findParcel(id);
                        Console.WriteLine(yoyo);
                        break;

                    case 5://return to the main menu
                        break;
                    default:
                        Console.WriteLine("ERROR! Please enter a valid value");
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

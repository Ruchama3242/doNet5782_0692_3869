using System;
using IDAL.DO;
using IDAL.DO.DalObject;

namespace ConsoleUI
{

    class Program
    {
        enum option { add=1, update, display, viewList, exit };
        static void Main(string[] args)
        {
            DalObject da =new DalObject();
            int mainInput;
            Console.WriteLine("Choose one of the following options:\n" +
                              "for insert options, press 1\n"+
                              "for update options, press 2\n"+
                              "for display options press 3\n"+
                              "For options for viewing the lists, press 4\n"+
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

        /// <summary>
        /// including al the adding cases
        /// </summary>
         static void   addOption()
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
                case 1:
                    Station temp = new Station();
                    string nameStation;
                    int idStation, charge;
                    double longitudeStation, lattitudeStation;
                    Console.WriteLine("Enter the name of the station");
                    nameStation = Console.ReadLine();
                    Console.WriteLine("Enter the ID of the station");
                    idStation = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the longitude of the station");
                    longitudeStation = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the lettitude of the station");
                    lattitudeStation = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the charge slot of the station");
                    charge = int.Parse(Console.ReadLine());
                    temp.name = nameStation;
                    temp.ID = idStation;
                    temp.lattitude = lattitudeStation;
                    temp.longitude = longitudeStation;
                    temp.chargeSlots = charge;
                    DalObject.addStations(temp);
                    break;

                case 2:
                    Drone myDrone = new Drone();
                    int myId, myModel;
                    double battery;
                    int myWeight,myStatus;
                    Console.WriteLine("Enter the id of the drone");
                    myId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the model of the drone");
                    myModel = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the weight category of the drone (0 to light, 1 to medium, 2 to heavy)");
                    myWeight = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the drone status ( 0 for available, 1 for maintenace, 2 for delivery)");
                    myStatus = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the battery percentage");
                    battery = double.Parse(Console.ReadLine());
                    myDrone.ID = myId;
                    myDrone.model = myModel;
                    myDrone.weight =(WeightCategories)myWeight;
                    myDrone.status = (DroneStatus)myStatus;
                    myDrone.battery = battery;
                    DalObject.addDrone(myDrone);
                    break;

                case 3:
                    Customer women= new Customer();
                    int womenID;
                    string name, phone;
                    double longitude, lattitude;
                    Console.WriteLine("Enter the name of the customer");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter the id of the customer");
                    womenID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the phone of the customer");
                    phone= Console.ReadLine();
                    Console.WriteLine("Enter the longitude");
                    longitude = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the lattitude");
                    lattitude = double.Parse(Console.ReadLine());
                    women.ID = womenID;
                    women.name = name;
                    women.lattitude = lattitude;
                    women.longitude = longitude;
                    DalObject.addCustomer(women);
                    break;
                    
                case 4:
                    Parcel rut = new Parcel();
                    int id, senderId, targetId, whight, priority, droneId;
                    DateTime requested, scheduled, pickedUp, delivered;
                    Console.WriteLine("Enter the id of the percal");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the  sender id of the percal");
                    senderId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the  target id of the percal");
                    targetId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the weight category of the percal (0 to light, 2 to medium, 3 to heavy");
                    whight = int.Parse(Console.ReadLine()); ;
                    Console.WriteLine("Enter the prioriyt of the percal (0 for normal, 1 for fast, 2 for emergency");
                    priority = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the id of the drone");
                    droneId = int.Parse(Console.ReadLine());
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
                    rut.priority = (Priorities)priority;
                    rut.requested = requested;
                    rut.scheduled = scheduled;
                    rut.senderID = senderId;
                    rut.targetId = targetId;
                    rut.weight = (WeightCategories)whight;
                    DalObject.addParcel(rut);
                    break;

                case 5: //return to the main menu
                    break;
                default:
                    Console.WriteLine("ERROR! Please enter a valid value");
                    break;
            }
        }
        /// <summary>
        /// including all the update cases
        /// </summary>
        static void updateOption()
        {

            int input;
            Console.WriteLine("To assign a percal to the drone press 1\n" +
                              "for collection of a package by drone press 2\n" +
                              "To deliver a percal to the customer press 3\n" +
                              "for sending a drone for charging press 4\n" +
                              "to release a drone from a charger press 5\n" +
                              "To return to the main menu, press 6");
            input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 1:
                    int percalId, droneID;
                    Console.WriteLine("Enter the id of the percal");
                    percalId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the  id of the drone");
                    droneID= int.Parse(Console.ReadLine());
                    DalObject.ParcelDrone(percalId, droneID);
                    break;

                case 2:
                    int parcelID;
                    DateTime time;
                    Console.WriteLine("Enter the id of the percal");
                    parcelID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the time for the collection");
                    time = DateTime.Parse(Console.ReadLine());
                    DalObject.ParcelPickedUp( parcelID, time);
                    break;

                case 3:
                    int idParcel;
                    string date;
                    Console.WriteLine("Enter the id of the percal");
                    idParcel = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the time the package collected, at format 2011-03-21");
                    date = Console.ReadLine();
                    DateTime DDay = DateTime.Parse(date);
                    DalObject.ParcelReceived(idParcel, DDay);
                    break;

                case 4:
                    int idDrone, idStation;
                    Console.WriteLine("Enter the id of the drone");
                    idDrone = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the id of the station");
                    idStation = int.Parse(Console.ReadLine());
                    DalObject.SendToCharge(idDrone, idStation);
                    break;
                case 5:
                    int droneId, stationId;
                    DroneCharge buzz = new DroneCharge();
                    Console.WriteLine("Enter the id of the drone");
                    droneId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the id of the station");
                    stationId = int.Parse(Console.ReadLine());
                    buzz.droneID = droneId;
                    buzz.stationeld = stationId;
                    DalObject.BatteryCharged(buzz);
                    break;

                case 6: //return to the main menu
                    break;
                default:
                    Console.WriteLine("ERROR! Please enter a valid value");
                    break;
            }
        }
        /// <summary>
        /// including all the display cases
        /// </summary>
        static void displayOption()
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
                case 1:
                    Console.WriteLine("Enter the id of the station");
                    id= int.Parse(Console.ReadLine());
                    Station loky = new Station();
                    loky = DalObject.printStation(id);
                    //loky.ToString();
                    Console.WriteLine(loky);
                    break;

                case 2:
                    Console.WriteLine("Enter the id of the drone");
                    id = int.Parse(Console.ReadLine());
                    Drone flafy = new Drone();
                    flafy = DalObject.printDrone(id);
                    //flafy.ToString();
                    Console.WriteLine(flafy);
                    break;

                case 3:
                    Console.WriteLine("Enter the id of the customer");
                    id = int.Parse(Console.ReadLine());
                    Customer anonimy = new Customer();
                    anonimy = DalObject.printCustomer(id);
                    //anonimy.ToString();
                    Console.WriteLine(anonimy);
                    break;

                case 4:
                    Console.WriteLine("Enter the id of the parcel");
                    id = int.Parse(Console.ReadLine());
                    Parcel yoyo = new Parcel();
                    yoyo = DalObject.printParcel(id);
                    //yoyo.ToString();
                    Console.WriteLine(yoyo);
                    break;

                case 5://return to the main menu
                    break;
                default:
                    Console.WriteLine("ERROR! Please enter a valid value");
                    break;
            }
        }
        /// <summary>
        /// including all the view list cases
        /// </summary>
        static void viewListOption()
        {
            int input;
            Console.WriteLine("for list of station press 1\n" +
                              "for list of drones press 2\n" +
                              "for list of customers press 3\n" +
                              "for list of percals press 4\n" +
                              "for list of percal that have not yet been assigned to the drone press 5\n" +
                              "for displaying base stations with available charging stations press 6\n" +
                              "To return to the main menu, press 7");
           
            input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 1:
                    Station[] stationArr;
                    stationArr = DalObject.printAllStations();
                    foreach (Station item in stationArr)
                        Console.WriteLine(item.ToString());
                    break;

                case 2:
                    Drone[] droneArr;
                    droneArr = DalObject.printAllDrones();
                    foreach (Drone item in droneArr)
                        Console.WriteLine(item.ToString());
                    break;

                case 3:
                    Customer[] customerArr;
                    customerArr = DalObject.printAllCustomers();
                    foreach (Customer item in customerArr)
                            Console.WriteLine(item.ToString());
                    break;

                case 4:
                    Parcel[] parcelArr;
                    parcelArr = DalObject.printAllParcels();
                    foreach (Parcel item in parcelArr)
                        Console.WriteLine(item.ToString());
                    break;

                case 5:
                    Parcel[] NoParcelArr;
                    NoParcelArr = DalObject.printParcelsWithoutDrone();
                    foreach (Parcel item in NoParcelArr)
                        Console.WriteLine(item.ToString());
                    break;
                case 6:
                    Station[] avilableStations;
                    avilableStations = DalObject.printStationsWithChargeSlots();
                    foreach (Station item in avilableStations)
                        Console.WriteLine(item.ToString());
                    break;
                case 7://return to the main menu
                    break;
                default:
                    Console.WriteLine("ERROR! Please enter a valid value");
                    break;
            }
        }
    }
}

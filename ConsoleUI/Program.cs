using System;
using IDAL.DO;
using IDAL.DO.DalObject;

namespace ConsoleUI
{

    class Program
    {
        Customer c;
        //dalObject d=new dalObject();
       
        enum option { add, update, display, viewList, exit };
        static void Main(string[] args)
        {
            
            int mainInput;
            Console.WriteLine("Choose one of the following options: " +
                               "for insert options, press 0"+
                               "for update options, press 1"+
                               "for display options press 3"+
                               "For options for viewing the lists, press 4"+
                               "To exit, press 5");
            mainInput = int.Parse(Console.ReadLine());

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
                    Console.WriteLine("BY!");
                    break;
                default:
                    Console.WriteLine("ERROR! Please enter a valid value\n");
                    break;
            }
        }

        /// <summary>
        /// including al the adding cases
        /// </summary>
         static void   addOption()
        {
            int input;
            Console.WriteLine("To add a station, press 0" +
                              "To add a drone press 1" +
                              "To add a new customer press 2" +
                              "for receiving a package for delivery press 3" +
                              "To return to the main menu, press 4");
            input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 0:
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

                case 1:
                    Drone myDrone = new Drone();
                    int myId, myModel;
                    double battery;
                    int myWeight,myStatus;
                    Console.WriteLine("Enter the id of the drone");
                    myId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the model of the drone");
                    myModel = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the weight category of the drone (0 to light, 2 to medium, 3 to heavy");
                    myWeight = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the drone status ( 0 for available, 1 for maintenace, 2 for delivery");
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

                case 2:
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
                    Console.WriteLine("Enter the longitude ");
                    longitude = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the lattitude");
                    lattitude = double.Parse(Console.ReadLine());
                    women.ID = womenID;
                    women.name = name;
                    women.lattitude = lattitude;
                    women.longitude = longitude;
                    DalObject.addCustomer(women);
                    break;

                case 3:
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
                    Console.WriteLine("Enter the time of the requested at format......");
                    requested = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the time of the scheduled at format......");
                    scheduled = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the time the packge will collect, at format......");
                    pickedUp = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the time the packege delivered, at format......");
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
                case 4:
                    break;
                default:
                    Console.WriteLine("ERROR! Please enter a valid value\n");
                    break;
            }
        }
        /// <summary>
        /// including all the update cases
        /// </summary>
        static void updateOption()
        {

            int input;
            Console.WriteLine("To assign a percal to the drone press 0" +
                              "for collection of a package by drone press 1" +
                              "To deliver a percal to the customer press 2" +
                              "for sending a drone for charging press 3" +
                              "to release a drone from a charger press 4" +
                              "To return to the main menu, press 5");
            input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    Console.WriteLine("ERROR! Please enter a valid value\n");
                    break;
            }
        }
        /// <summary>
        /// including all the display cases
        /// </summary>
        static void displayOption()
        {
            int input;
            Console.WriteLine("for station press 0" +
                                      "for drone press 1" +
                                      "for customer press 2" +
                                      "for percal press 3" +
                                      "To return to the main menu, press 4");
            input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    Console.WriteLine("ERROR! Please enter a valid value\n");
                    break;
            }
        }
        /// <summary>
        /// including all the view list cases
        /// </summary>
        static void viewListOption()
        {
            int input;
            Console.WriteLine("for list of stationד press 0" +
                                      "for list of drones press 1" +
                                      "for list of customers press 2" +
                                      "for list of percals press 3" +
                                      "for list of percal that have not yet been assigned to the drone press 4" +
                                      "for displaying base stations with available charging stations press 5" +
                                      "To return to the main menu, press 6");
           
            input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                default:
                    Console.WriteLine("ERROR! Please enter a valid value\n");
                    break;
            }
        }
    }
}

using System;
using IBL.BO;
using BL.BO;
namespace ConsoleUI_BL
{
    class Program
    {
        enum option { add = 1, update, display, viewList, exit };
        static void Main(string[] args)
        {

            //BL myBL = new BL();
            
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

        static void addOption()
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

                    case 1:
                      IBL.BO.Station temp = new Station();
                        string nameStation;
                        int idStation, charge;
                        float longitudeStation, lattitudeStation;
                        Console.WriteLine("Enter the name of the station");
                        nameStation = Console.ReadLine();
                        Console.WriteLine("Enter the ID of the station");
                        idStation = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the longitude of the station");
                        longitudeStation = float.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the lettitude of the station");
                        lattitudeStation = float.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the charge slot of the station");
                        charge = int.Parse(Console.ReadLine());
                        temp.name = nameStation;
                        temp.ID = idStation;
                        temp.lattitude = lattitudeStation;
                        temp.longitude = longitudeStation;
                        temp.chargeSlots = charge;

                        d.addStations(temp);

                        break;

                    case 2:
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

                    case 3:
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

                    case 4:
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
                        int newId = d.addParcel(rut);
                        Console.WriteLine("The id of the parcel is " + newId + "\n");
                        break;

                    case 5: //return to the main menu
                        break;
                    default:
                        Console.WriteLine("ERROR! Please enter a valid value");
                        break;
                }
            }
            catch (IdUnExistsException e)
            {
                Console.WriteLine(e);
            }
            catch (IdExistsException e)
            {
                Console.WriteLine(e);
            }
            catch (generalException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

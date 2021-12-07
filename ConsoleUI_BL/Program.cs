using System;
using System.Collections.Generic;
using System.Collections;
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


            IBL.IBL myIBL = new BL.BL();
            
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
                    case (int)option_a.add:
                        addOption(myIBL);
                        break;
                    case (int)option_a.update:
                        updateOption(myIBL);
                        break;
                    case (int)option_a.display:
                        displayOption(myIBL);
                        break;
                    case (int)option_a.viewList:
                        viewListOption(myIBL);
                        break;
                    case (int)option_a.exit:
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

        static void addOption(IBL.IBL myIBL)
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
                        Console.WriteLine("Enter the charge slots of the station");
                        chargeEmpty = int.Parse(Console.ReadLine());
                        temp.name = nameStation;
                        temp.ID = idStation;
                        temp.location = new Location 
                        { latitude = lattitudeStation, longitude = longitudeStation };
                        temp.chargeSlots = chargeEmpty;
                        myIBL.addStation(temp);
                        break;

                    case (int)option_b.drone:
                        
                        int myId,stationID, myModel;
                     
                        int myWeight;
                        Console.WriteLine("Enter the id of the drone");
                        myId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter the id of the station(for thr first charge)");
                        stationID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the model of the drone");
                        myModel = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the weight category of the drone (0 to light, 1 to medium, 2 to heavy)");
                        myWeight = int.Parse(Console.ReadLine());
                    
                        myIBL.addDrone(myId,myModel,myWeight,stationID);
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
                        women.location = new Location { latitude = lattitude, longitude = longitude };
                        myIBL.addCustomer(women);
                        break;

                    case (int)option_b.parcel:
                        //Parcel rut = new Parcel();
                        int id, senderId, targetId,weight , priority; 
                        id = 0; //int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the sender id of the percal");
                        senderId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the  target id of the percal");
                        targetId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the weight category of the percal (0 to light, 1 to medium, 2 to heavy)");
                        weight = int.Parse(Console.ReadLine()); ;
                        Console.WriteLine("Enter the prioriyt of the percal (0 for normal, 1 for fast, 2 for emergency)");
                        priority = int.Parse(Console.ReadLine());
                        //rut.ID = id;
                        //rut.priority = (Priorities)priority;
                        //rut.senderID = senderId;
                        //rut.targetId = targetId;
                        //rut.weight = (WeightCategories)whight;
                        int newId = myIBL.addParcel(senderId,targetId,weight,priority);
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

        static void updateOption(IBL.IBL myIBL)
        {
            try
            {
                int input;
                Console.WriteLine("To update the name of the drone press 1\n" +
                                  "for update ditail of station 2\n" +
                                  "To update ditail of customer press 3\n" +
                                  "for send a drone to charge press 4\n" +
                                  "to release a drone from charge press 5\n" +
                                  "To set a drone for a parcel press 6\n"+
                                  "For collection of a package by drone press 7\n"+
                                  "Delivery of a package by drone press 8\n" +
                                  "To return to the main menu, press 9 ");
                input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        int  droneID;
                        int newName;
                        Console.WriteLine("Enter the id of the drone");
                        droneID = int.Parse(Console.ReadLine());
                        Console.WriteLine("ener the new name for the drone");
                        newName = int.Parse(Console.ReadLine());
                        myIBL.updateNameDrone(droneID,newName);
                        break;

                    case 2:
                        int stID;string name; int sum;
                        Console.WriteLine("Enter the id of the station");
                        stID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new name for the station,or enter");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter the number of new charging stations, to keep the previous data enter 0");
                        sum = int.Parse(Console.ReadLine());
                        myIBL.updateStation(stID, name, sum);
                        break;

                    case 3:
                        int id;
                        string n, phone;
                        Console.WriteLine("Enter the id of the customer");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new name for the customer, or click enter");
                        n = Console.ReadLine();
                        Console.WriteLine("enter the new  phone number or click enter ");
                        phone = Console.ReadLine();
                        myIBL.updateCustomer(id, n, phone);
                        break;

                    case 4:
                        int droneId;
                        Console.WriteLine("Enter the id of the drone");
                        droneId = int.Parse(Console.ReadLine());
                        myIBL.sendToCharge(droneId);
                        break;

                    case 5:
                        int drID, time;
                        Console.WriteLine("Enter the id of the drone");
                        drID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the loading time in minutes");
                        time = int.Parse(Console.ReadLine());
                        myIBL.releaseFromCharge(drID, time);
                        break;

                    case 6:
                        int i;
                        Console.WriteLine( "enter the id of the drone");
                        i = int.Parse(Console.ReadLine());
                        myIBL.parcelToDrone(i);
                        break;

                    case 7:
                        int j;
                        Console.WriteLine("enter the id of the drone");
                        j = int.Parse(Console.ReadLine());
                        myIBL.packageCollection(j);
                        break;

                    case 8:
                        int a;
                        Console.WriteLine("enter the id of  the drone");
                        a = int.Parse(Console.ReadLine());
                        myIBL.packageDelivery(a);
                        break;

                    case 9: //return to the main menu
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

        static void displayOption(IBL.IBL myIBL)
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

        static void viewListOption(IBL.IBL myIBL)
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
                case (int)option_b.station:
                    IEnumerable<StationToList> stationList = new List<StationToList>();
                    stationList = myIBL.veiwListStation();
                    foreach (var item in stationList)
                        Console.WriteLine(item.ToString());
                    break;

                case (int)option_b.drone:
                    IEnumerable<DroneToList> droneList = new List<DroneToList>();
                    droneList = myIBL.getAllDrones();
                    foreach (var item in droneList)
                        Console.WriteLine(item.ToString());
                    break;

                case (int)option_b.customer:
                    IEnumerable<CustomerToList> customerList = new List<CustomerToList>();
                    customerList = myIBL.viewListCustomer();
                    foreach (var item in customerList)
                        Console.WriteLine(item.ToString());
                    break;

                case (int)option_b.parcel:
                    IEnumerable<ParcelToList> parcelList = new List<ParcelToList>();
                    parcelList = myIBL.getAllParcels();
                    foreach (var item in parcelList)
                        Console.WriteLine(item.ToString());
                    break;

                case 5:
                    IEnumerable<ParcelToList> noParcelList = new List<ParcelToList>();
                    noParcelList = myIBL.parcelsWithoutDrone();
                    foreach (var item in noParcelList)
                        Console.WriteLine(item.ToString());
                    break;
                case 6:
                    IEnumerable<Station> avilableStations = new List<Station>();
                    avilableStations = myIBL.avilableCharginStation();
                    foreach (var item in avilableStations)
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

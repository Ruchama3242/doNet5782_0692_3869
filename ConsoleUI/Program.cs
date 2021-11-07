﻿//We did one bonus of a sexagsimal display
using System;
using IDAL.DO;
using IDAL.DO.DalObject;
using System.Collections.Generic;
using System.Collections;


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
                        addOption(da);
                        break;
                    case (int)option.update:
                        updateOption(da);
                        break;
                    case (int)option.display:
                        displayOption(da);
                        break;
                    case (int)option.viewList:
                        viewListOption(da);
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
         static void   addOption(DalObject d)
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
                    myDrone.weight =(WeightCategories)myWeight;
                    //myDrone.status = (DroneStatus)myStatus;
                    //myDrone.battery = battery;
                    d.addDrone(myDrone);
                    break;

                case 3:
                    Customer women= new Customer();
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
                    int newId=d.addParcel(rut);
                    Console.WriteLine("The id of the parcel is "+newId+"\n");
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
        static void updateOption(DalObject d)
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
                    d.ParcelDrone(percalId, droneID);
                    break;

                case 2:
                    int parcelID;
                    DateTime time;
                    Console.WriteLine("Enter the id of the percal");
                    parcelID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the time for the collection");
                    time = DateTime.Parse(Console.ReadLine());
                    d.ParcelPickedUp( parcelID, time);
                    break;

                case 3:
                    int idParcel;
                    string date;
                    Console.WriteLine("Enter the id of the percal");
                    idParcel = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the time the package collected, at format 2011-03-21");
                    date = Console.ReadLine();
                    DateTime DDay = DateTime.Parse(date);
                    d.ParcelReceived(idParcel, DDay);
                    break;

                case 4:
                    int idDrone, idStation;
                    Console.WriteLine("Enter the id of the drone");
                    idDrone = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the id of the station");
                    idStation = int.Parse(Console.ReadLine());
                    d.SendToCharge(idDrone, idStation);
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
                    d.BatteryCharged(buzz);
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
        static void displayOption(DalObject d)
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
                    try
                    {
                        loky = d.printStation(id);
                        Console.WriteLine(loky);
                        sexagesimal(loky.longitude, loky.lattitude);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0}", e);
                    }
                    break;

                case 2:
                    Console.WriteLine("Enter the id of the drone");
                    id = int.Parse(Console.ReadLine());
                    Drone flafy = new Drone();
                    try
                    {
                        flafy = d.printDrone(id);
                        Console.WriteLine(flafy);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0}", e);
                    }
                    break;

                case 3:
                    Console.WriteLine("Enter the id of the customer");
                    id = int.Parse(Console.ReadLine());
                    Customer anonimy = new Customer();
                    try
                    {
                        anonimy = d.printCustomer(id);
                        Console.WriteLine(anonimy);
                        sexagesimal(anonimy.longitude, anonimy.lattitude);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0}", e);
                    }
                    break;

                case 4:
                    Console.WriteLine("Enter the id of the parcel");
                    id = int.Parse(Console.ReadLine());
                    Parcel yoyo = new Parcel();
                    try
                    {
                        yoyo = d.printParcel(id);
                        Console.WriteLine(yoyo);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0}", e);
                    }
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
        static void viewListOption(DalObject d)
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
                    IEnumerable<Station> stationList = new List<Station>();
                    stationList = d.printAllStations();
                    foreach (Station item in stationList)
                    {
                        Console.WriteLine(item.ToString());
                        sexagesimal(item.longitude, item.lattitude);
                        Console.WriteLine();
                    }
                    break;

                case 2:
                    IEnumerable<Drone> droneList = new List<Drone>();
                    droneList = d.printAllDrones();
                    foreach (Drone item in droneList)
                        Console.WriteLine(item.ToString());
                    break;

                case 3:
                    IEnumerable<Customer> customerList = new List<Customer>();
                    customerList = d.printAllCustomers();
                    foreach (Customer item in customerList)
                    {
                        Console.WriteLine(item.ToString());
                        sexagesimal(item.longitude, item.lattitude);
                        Console.WriteLine();
                    }
                    break;

                case 4:
                    IEnumerable<Parcel> parcelList = new List<Parcel>();
                    parcelList = d.printAllParcels();
                    foreach (Parcel item in parcelList)
                        Console.WriteLine(item.ToString());
                    break;

                case 5:
                    IEnumerable<Parcel> noParcelList = new List<Parcel>();
                    noParcelList = d.printParcelsWithoutDrone();
                    foreach (Parcel item in noParcelList)
                        Console.WriteLine(item.ToString());
                    break;
                case 6:
                    IEnumerable<Station> avilableStations = new List<Station>();
                    avilableStations = d.printStationsWithChargeSlots();
                    foreach (Station item in avilableStations)
                    {
                        Console.WriteLine(item.ToString());
                        sexagesimal(item.longitude, item.lattitude);
                    }
                    break;

                case 7://return to the main menu
                    break;
                default:
                    Console.WriteLine("ERROR! Please enter a valid value");
                    break;
            }
        }
         static void sexagesimal(float longitude,float lattitude )
        {
            int longSeconds = (int)Math.Round(longitude * 60 * 60);
            double x = (longitude - Math.Truncate(longitude)) * 60;//the decimaly part *60
            float seconds = (float)(x - Math.Truncate(x))*60;//the decimaly part of minute *60
            int minutes = ((longSeconds / 60) % 60);
            int degrees = ((longSeconds / 60) / 60);
            //
            if (seconds < 0)
                seconds = seconds * -1;
            if (minutes < 0)
                minutes = minutes * -1;
            if (degrees < 0)
                degrees = degrees * -1;

           // Console.WriteLine(@"{0}° {1}' {2}""E", degrees,minutes,seconds);//latitude

            int latSeconds = (int)Math.Round(lattitude * 60 * 60);
            double xL = (lattitude - Math.Truncate(lattitude)) * 60;//the decimaly part *60
            float secondsL = (float)(xL - Math.Truncate(xL)) * 60;//the decimaly part of minute *60
            int minutesL = (latSeconds / 60) % 60; 
            int degreesL = (latSeconds / 60) / 60;
            //
            if (secondsL < 0)
                secondsL = secondsL * -1;
            if (minutesL < 0)
                minutesL = minutesL * -1;
            if (degreesL < 0)
                degreesL = degreesL * -1;

            Console.WriteLine($@"the cordinate at sexagismal: {degrees}° {minutes}' {seconds}"" E ,{degreesL}° {minutesL}' {secondsL}"" S"); 
                //degrees, minutes, seconds,degreesL, minutesL, secondsL);
        }
    }
}

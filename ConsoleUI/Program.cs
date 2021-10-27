using System;
using IDAL.DO;
using IDAL.DO.DalObject;

namespace ConsoleUI
{

    class Program
    {
        Customer c;
        dalObject d=new dalObject();
       
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
                    Console.WriteLine("Enter the name of the station");
                    string nameStation;
                    nameStation = Console.ReadLine();
                   // dalObject.
                    //dalObject.addStation(nameStation);
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

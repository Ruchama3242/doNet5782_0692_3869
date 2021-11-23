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
    }
}

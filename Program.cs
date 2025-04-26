using Microsoft.Extensions.DependencyInjection;

public class Program{

    //Add function mainly only displays user interface. Actual function to add tasks is in TaskList class.
    static async Task addList(ITaskList list, string file){
        Console.Write("Please enter task title: ");
        string title = Console.ReadLine();
        DateTime date = new DateTime();
        while (true){
            try{
                Console.Write("Please enter task due date (yyyy-mm-dd): ");
                date = DateTime.Parse(Console.ReadLine());
                break;
            } catch (Exception){
                Console.WriteLine("Please enter a valid date!\n");
            }
        }
        await list.addTask(new TaskItem {title = title, dueDate = date}, file);
        Console.WriteLine("Task has been Added!");
        //While I could just directly grab the final Id with "list.tasks.Last().id", this would break encapsulation.
        list.viewTaskId(list.GetLastTaskId());
        Console.WriteLine("");
        return;
    }

    //Edit function mainly only displays user interface. Actual function to edit tasks is in TaskList class.
    static void editList(ITaskList list){
        int idEdit;
        while (true){
            try{
                list.viewTasks();
                Console.WriteLine("Which task would you like to edit? (Type \"0\" to return to main menu):");
                idEdit = int.Parse(Console.ReadLine());
                if (idEdit == 0){
                    Console.WriteLine("Returning to main menu!\n");
                    return;
                } else if (!list.hasId(idEdit)){
                    Console.WriteLine("\nNo task has that id!\n");
                } else{
                    break;
                }
            } catch (Exception){
                Console.WriteLine("\nPlease enter a valid id!\n");
            }              
        }
        
        Console.WriteLine("Please enter a new title. Leave blank to use the same title.");
        string titleEdit = Console.ReadLine();
        string temp;
        DateTime dateEdit = new DateTime();
        while (true){
            try{
                Console.WriteLine("Please enter a new due date. Leave blank to use the same due date.");
                temp = Console.ReadLine();
                if (temp != ""){
                    dateEdit = DateTime.Parse(temp);
                }
                break;
            } catch (Exception){
                Console.WriteLine("Please enter a valid date!\n");
            }
        }
        bool completeEdit = false;
        while (true){
            try{
                Console.WriteLine("Is the test completed? (true/false).");
                completeEdit = bool.Parse(Console.ReadLine());
                break;
            } catch (Exception){
                Console.WriteLine("Please enter a valid status!\n");
            }
        }
        
        list.editTask(idEdit, titleEdit, dateEdit, completeEdit);
        list.viewTaskId(idEdit);
        Console.WriteLine("");
        return;
    }

    //Delete function mainly only displays user interface. Actual function to delete tasks is in TaskList class.
    static void deleteList(ITaskList list){
        int idView;
        while (true){
            try{
                list.viewTasks();
                Console.WriteLine("Which task would you like to delete? (Type \"0\" to return to main menu):");
                idView = int.Parse(Console.ReadLine());
                if (idView == 0){
                    Console.WriteLine("Returning to main menu!\n");
                    return;
                } else if (!list.hasId(idView)){
                    Console.WriteLine("\nNo task has that id!\n");
                } else{
                    break;
                }
            } catch (Exception){
                Console.WriteLine("\nPlease enter a valid id!\n");
            }              
        }
        list.viewTaskId(idView);
        list.deleteTask(idView);
        Console.WriteLine("Task has been Deleted!\n");
    }

    static void viewList(ITaskList list){
        list.viewTasks();
        Console.WriteLine("");
    }

    //Filter function mainly only displays user interface. Actual function to filter tasks is in TaskList class.
    static void filterList(ITaskList list){
        int input;
        while (true){
            try{
                Console.WriteLine("How would you like to filter the tasks?");
                Console.WriteLine("1. Sort by Incomplete tasks\n2. Sort by complete tasks\n3. Sort tasks by due date(Ascending)\n4. Sort tasks by due date(Descending)\n5. Sort tasks alphabetically (Ascending)\n6. Sort tasks alphabetically (Descending)");
                input = int.Parse(Console.ReadLine());
                if (input < 1 || input > 6){
                    Console.WriteLine("\nPlease enter a valid id!\n");
                } else{
                    break;
                }
            } catch (Exception){
                Console.WriteLine("\nPlease enter a valid id!\n");
            }
        }
        list.filterTasks(input);
        return;
    }

    //Fills task list with several Tasks. Only used for debuging and not used in final program.
    public static async Task debugFunc(ITaskList list, string file){
        await list.addTask(new TaskItem{title = "abc", dueDate = new DateTime(2006,11,21), isCompleted = true}, file);
        await list.addTask(new TaskItem{title = "xyz", dueDate = new DateTime(2003,05,22), isCompleted = false}, file);
        await list.addTask(new TaskItem{title = "123", dueDate = new DateTime(2004,11,01), isCompleted = true}, file);
        await list.addTask(new TaskItem{title = "098", dueDate = new DateTime(2001,02,03), isCompleted = false}, file);
        await list.addTask(new TaskItem{title = "12pldoa", dueDate = new DateTime(2022,01,16), isCompleted = true}, file);
        await list.addTask(new TaskItem{title = "dsfjk", dueDate = new DateTime(2016,09,12), isCompleted = false}, file);
    }
    
    //Main Function
    static async Task Main(string[] args){
        
        //Generate Empty Task list in a ServiceCollention. This allows for modularity as if the TaskList class needs to be changed in the future, 
        //all other functioning code can stay the same in Program.cs as long as the replaceing class implements the same interface.
        var services = new ServiceCollection();
        services.AddSingleton<ITaskList, TaskList>();
        var provider = services.BuildServiceProvider();
        var list = provider.GetRequiredService<ITaskList>();

        string file = "tasks.json";

        //Funtion used for debugging. Not used in final program.
        //await debugFunc(list, file);
        Console.WriteLine("Welcome to the Task Manager!");
        if (File.Exists(file)){
            while (true){
                Console.WriteLine("It seems like a set of tasks already exists! Would you like to load them in? (1 for yes, 2 for no)");
                var confimation = Console.ReadLine();
                if (confimation == "1"){
                    Console.WriteLine("Loading previous tasks...");
                    await list.loadFromJson(file);
                    break;
                } else if (confimation == "2"){
                    Console.WriteLine("Proceeding without previous tasks...");
                    break;
                } else {
                    Console.WriteLine("Not a valid answer!");
                }
            }
        }

        //Loop through menus until program is closed.
        while (true){
            Console.WriteLine("Enter the number for the task you want to execute:");
            Console.WriteLine("1. Add Task\n2. Edit Task\n3. Delete Task\n4. View All Tasks\n5. Filter Tasks\n6. Save and Exit");
            var input = Console.ReadLine();
            Console.WriteLine("");
            switch(input){
                case "1":
                    await addList(list, file);
                    break;
                case "2":
                    editList(list);
                    break;
                case "3":
                    deleteList(list);
                    break;
                case "4":
                    viewList(list);
                    break;
                case "5":
                    filterList(list);
                    break;
                case "6":
                    Console.WriteLine("Exiting Task Manager!");
                    return;     
                default:
                    Console.WriteLine("That is not a valid option!");
                    Console.WriteLine("");
                    break;
            }
        }
    }
}
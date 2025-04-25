public class Program{

    static void editFunction(TaskList list){
        int idEdit;
        while (true){
            try{
                list.viewTasks();
                Console.WriteLine("Which task would you like to edit? (Type \"0\" to return to main menu):");
                idEdit = int.Parse(Console.ReadLine());
                if (idEdit == 0){
                    Console.WriteLine("Returning to main menu!\n");
                    return;
                } else if (list.hasId(idEdit)){
                    Console.WriteLine("No task has that id!\n");
                } else{
                    break;
                }
            } catch (Exception e){
                Console.WriteLine("Please enter a valid id!\n");
            }
                        
        }
                   
        Console.WriteLine("Please enter a new title. Leave blank to use the same title.");
        string titleEdit = Console.ReadLine();
        Console.WriteLine("Please enter a new due date. Leave blank to use the same due date.");
        DateTime dateEdit = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Is the test completed? Leave blank to keep status the same (true/false).");
        bool completeEdit = bool.Parse(Console.ReadLine());
        list.editTask(idEdit, titleEdit, dateEdit, completeEdit);
        list.viewTaskId(idEdit);
        Console.WriteLine("");
        return;
    }
    
    static void Main(string[] args){

        TaskList list = new TaskList();

        Console.WriteLine("Welcome to the Task Manager! \nEnter the number for the task you want to execute:");

        while (true){
            Console.WriteLine("Enter the number for the task you want to execute:");
            Console.WriteLine("1. Add Task\n2. Edit Task\n3. Delete Task\n4. View All Tasks\n5. Filter Tasks\n6. Save and Exit");
            var input = Console.ReadLine();
            Console.WriteLine("");
            switch(input){
                case "1":
                    Console.Write("Please enter task title: ");
                    string title = Console.ReadLine();
                    DateTime date = new DateTime();
                    while (true){
                        try{
                            Console.Write("Please enter task due date (yyyy-mm-dd): ");
                            date = DateTime.Parse(Console.ReadLine());
                            break;
                        } catch (Exception e){
                            Console.WriteLine("Please enter a valid date!\n");
                        }
                    }
                    list.addTask(new Task {title = title, dueDate = date});
                    Console.WriteLine("Task has been Added!");
                    list.viewTaskId(list.tasks.Count);
                    Console.WriteLine("");
                    break;
                case "2":
                    editFunction(list);
                    break;
                case "3":
                    list.viewTasks();
                    Console.WriteLine("Which task would you like to delete?:");
                    int idView = int.Parse(Console.ReadLine());
                    list.viewTaskId(idView);
                    list.deleteTask(idView);
                    Console.WriteLine("Task has been Deleted!\n");
                    break;
                case "4":
                    list.viewTasks();
                    Console.WriteLine("");
                    break;
                case "5":
                    Console.WriteLine("TESTING!");
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

        list.viewTasks();

    }

}
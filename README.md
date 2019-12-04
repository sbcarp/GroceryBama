# DEMO
Open https://grocerybama.com/

IE is not recommended

# How To Run (Binary)
System Reuqirements: Windows 7 or higher

Step 1. Download and install .NET Core 3.0 hosting bundle: [[Download](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-aspnetcore-3.0.1-windows-hosting-bundle-installer)]

Step 2. Disable windows firewalls & and antivirus if necessary

Step 3. Import the dump file in MySQL Workbench, don't run it in script window [[File](https://drive.google.com/file/d/1yf4fUjfV7zZuAH3G6-4kyf-3oMoLrS9m/view?usp=sharing)]

Note: make sure the port number of the instance is the default one: 3306

Step 4. Go to the folder GroceryBama\bin\Release\netcoreapp3.0\publish\, Run GroceryBama.exe as administrator

Step 5. Open the localhost url in browser with displayed port number


# Build Requirements
- Visual Studio 2019 [[Download](https://visualstudio.microsoft.com/vs/ "Download")]
	- With ASP.NET and web development Workload installed
	- GitHub extension for Visual Studio (Optional)
- MySQL 8.0 [[Download](https://dev.mysql.com/downloads/mysql/ "Download")]
	- MySQL 8.0 Workbench CE (Optional)

# Configurations
### MySQL Configuration
It is recommended to use the following settings during installation

`High Availability` Standalone MySQL Server / Classic MySQL Replication

`Authentication Method`  Use Legacy Authentication Method

Then use the scripts above to create a new user and database, if you want to use names or credentials other than default, remember to change the file `AppInformation.cs` under project root folder and exclude it in `.gitignore`.


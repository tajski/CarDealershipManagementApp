In Database backup, there is a backup of project's database ready for use. 
Only thing to set up is a connectionString in Visual studio. 
It basically contains "Server Name" found in Properties on the right when we open any table in MS SQL Server Management Studio. 
Then we use it when setting up new connection in Visual Studio, and later we can copy Connection String from down-right panel called Properties. After that, we right click on project name in Solution Explorer -> Properties -> Resources -> we name a String "connectionString" and in Value we paste the content.
After that we are ready to go :)


PS. already created accounts for fictional employees are:

USERNAME			PASSWORD
admin				123
avera				avera
jkowal				jkowal
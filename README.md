# SQLFactory
A small library inspired by the Django model system, focused on creating sql code from table models from c# native code.
##Table of contents 
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
* [User Manual](#user-manual)
## General info
This project creates and sql code from a model created in c# native code
## Technologies
Project is created with:
* Visual Studio 2019
* .NET 5.0
* C# 9.0
## Setup
  First download the [zip](https://github.com/aldomalaca1/SQLFactory/blob/master/SQLFactory.rar) of v 1.0.0 and after unzip it wherever you want, just remember the path.
  The second thing that you must to do is follow this instructions
  ###  Instructions
  * On the right side of your current project go to Dependencies in the solution explorer, left click and choose the option "Add reference to project"
  
  ![Step one](https://i.gyazo.com/3ac62f69a6c320378fb6d44274be1b90.png)
  
  * In the window that just opened, click on explore/examine.
  
  ![Step two](https://i.gyazo.com/144d5cfb8a4eb576d479cb923624ff7b.png)
  
  * Inside the library folder look for the file bin > debug > net5.0 > ref > SQFactory.dll and click add 
  
  ![Step tree](https://i.gyazo.com/73d765378a06b7d944b6e5cd7c9f1721.png)
  
  * Now just click accept
  
  ![Step four](https://i.gyazo.com/a0c806b8402d30747b90be5d01d33dab.png)
  
  * Just add it like any library in your project, and you're good to go.
  
  ![Step firve](https://i.gyazo.com/d81492b558b82f4a1f23cdc89161d34e.png)
  
  ## User Manual
  
  * Create a model table model inheriting from  ```c# SQLModels.SQLModel``` 
  
  ```c#
   class TestModel1: SQLModels.SQLModel{
        string id = Attributes.Stick(field: Integer(), notNull: true, primaryKey: false, autoincrement: false, unique: true);
        string campo1 = Varchar(140);
        string campo2 = Text();
        string campo3 = Blob();
        string campo4 = Real();
    }
```

* If you want to add an primary key just only change the "primaryKey" attribute from "false" to "true"

### Note
* You can autoincrement the primary key changing the "autoincremente" atribute to true, but remember that this option It only works in conjunction with the primary key, 
if you use it without the attribute of the primary key a defective code will be generated that will give error when executing it in your database

* If you want to add more than a primary key you need to create an special model for that that inherits from ```c# SQLModels.Keys``` like this:
  
```c#
   class KeysModel1 : SQLModels.Keys {
          string primaryKey = ComposedPrimaryKey(keyNames: new string[] {"id","campo2"});
    }
```
### End of note
* You can also create foreign keys, you just have to make another table model like the one we saw above and in the key model add the foreign keys in this way:

```c#
       class TestModel2ForeignTable: SQLModels.SQLModel{
        string foreignID = Attributes.Stick(field: Integer(), primaryKey: true);
        string campo1 = Varchar(140);
        string campo2 = Text();
    }
```

```c#

 class KeysModel2 : SQLModels.Keys{
         string foreignKey = ForeignKey(foreignKeyInTable:"foreignID", foreignKey: "foreignID", connectToTable: "TestModel2ForeignTable");
        //You can add more than foreign key in the model using the same logic 
    }
```

* Now you need to create the following object:

```c#
SQLTableAssembly testObj = new();
```

* Create the objecs for the models

```c#
TestModel1 testModel1 = new();
KeysModel1 keysModel1 = new ();
```

* Now these two objects you have to give to the function ```c# .assemblyIntoSQL``` like this:

```c#
string sql = testObj.assemblyIntoSQL(testModel1, keysModel1);
```

*If what you want is to simply send the model of the table and already, just put the following attribute as null

```c#
string sql = testObj.assemblyIntoSQL(testModel1, null);
```

This function will return a string with the sql instruction of the table so that it can be used in a database in sqlite.

And well, that would be it, I hope someone find this library helful. 


  
  
  

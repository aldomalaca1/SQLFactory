using System;
using SQLFactory;

namespace SQLFactoryTest
{   
    
    class TestModel1: SQLModels.SQLModel{
        string id = Attributes.Stick(field: Integer(), notNull: true, primaryKey: false, autoincrement: false, unique: true);
        string campo1 = Varchar(140);
        string campo2 = Text();
        string campo3 = Blob();
        string campo4 = Real();
    }

    class KeysModel1 : SQLModels.Keys {
          string primaryKey = ComposedPrimaryKey(keyNames: new string[] {"id","campo2"});
    }

    class TestModel2: SQLModels.SQLModel{
        string id = Attributes.Stick(field: Integer(), notNull: true, primaryKey: false, autoincrement: false, unique: true);
        string foreignID = Integer();
        string campo1 = Varchar(140);
        string campo2 = Text();
        string campo3 = Blob();
        string campo4 = Real();
    }

    class TestModel2ForeignTable: SQLModels.SQLModel{
        string foreignID = Attributes.Stick(field: Integer(), primaryKey: true);
        string campo1 = Varchar(140);
        string campo2 = Text();
    }

    class KeysModel2 : SQLModels.Keys{
        string foreignKey = ForeignKey(foreignKeyInTable:"foreignID", foreignKey: "foreignID", connectToTable: "TestModel2ForeignTable");
    }

    class TestModel3 : SQLModels.SQLModel{
        string id = Attributes.Stick(field: Integer(), notNull: true, primaryKey: false, autoincrement: false, unique: true);
        string model3Foreignid = Integer();
        string model3foreign2id = Integer();
        string campo1 = Varchar(140);
        string campo2 = Text();
        string campo3 = Blob();
        string campo4 = Real();
    }

    class TestModel3Foreign : SQLModels.SQLModel{
        string model3Foreignid = Attributes.Stick(field: Integer(), notNull: true, primaryKey: false, autoincrement: false, unique: true);
        string campo1 = Varchar(140);
        string campo2 = Text();
        string campo3 = Blob();
        string campo4 = Real();
    }

    class TestModel3Foreign2 : SQLModels.SQLModel{
        string model3foreign2id = Attributes.Stick(field: Integer(), notNull: true, primaryKey: false, autoincrement: false, unique: true);
        string campo1 = Varchar(140);
        string campo2 = Text();
        string campo3 = Blob();
        string campo4 = Real();
    }

    class KeysModel3 : SQLModels.Keys{
        string foreignKey = ForeignKey(foreignKeyInTable: "model3Foreignid", foreignKey: "model3Foreignid", connectToTable: "TestModel3Foreign");
        string foreignKey2 = ForeignKey(foreignKeyInTable: "model3foreign2id", foreignKey: "model3foreign2id", connectToTable: "TestModel3Foreign2");
    }

  


    class Program
    {
        static SQLTableAssembly testObj = new();
        
        static void Main(string[] args){

            TestModel1 testModel1 = new();
            KeysModel1 keysModel1 = new ();

            Console.WriteLine("Testing with basic table strcuture");
            Console.WriteLine(testObj.assemblyIntoSQL(testModel1, keysModel1));

            TestModel2 testModel2 = new();
            TestModel2ForeignTable foreignModelTable2 = new();
            KeysModel2 keysModel2 = new();

            Console.WriteLine("Testing with connected tables by foreign key");
            Console.WriteLine("Table1");
            Console.WriteLine(testObj.assemblyIntoSQL(testModel2, keysModel2));
            Console.WriteLine("Table2");
            Console.WriteLine(testObj.assemblyIntoSQL(foreignModelTable2, null));

            Console.WriteLine("Testing multiple foreign keys");
            TestModel3 testmodel3 = new();
            TestModel3Foreign testModel3Foreign = new();
            TestModel3Foreign2 testModel3Foreign2 = new();
            KeysModel3 keysModel3 = new();
            Console.WriteLine(testObj.assemblyIntoSQL(testmodel3, keysModel3));
            Console.WriteLine(testObj.assemblyIntoSQL(testModel3Foreign, null));
            Console.WriteLine(testObj.assemblyIntoSQL(testModel3Foreign2, null));





        }
    }
}

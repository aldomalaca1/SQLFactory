using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SQLFactory
{
    public class SQLTableAssembly{
        //Binding flags
        private BindingFlags flags =  BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        //Substract fields info of table/model
        private Dictionary<String, String> substractFields(object table){
            //Declare an dictionary for fields 
            Dictionary<String, String > fieldsData= new();
            foreach (FieldInfo field in table.GetType().GetFields(flags)){
                //Save the fiels name and values as string in the dictionary
                fieldsData.Add(field.Name, field.GetValue(table).ToString());
            }
            return fieldsData;
        }

        public String assemblyIntoSQL(SQLModels.SQLModel table, SQLModels.Keys keys){

            //Declaring a string variable for the sql instruction
            string sql = "";

            //Check if table is not null
            if (table != null){
                //Contruct the fist part of the sql code for a table
                sql = String.Format("create table {0} ( ", table.GetType().Name);

                //Iterate fields in table
                foreach (KeyValuePair<String, String> data in substractFields(table))
                {       //Construct the sql code to create only the table
                    sql += String.Format("{0} {1},", data.Key, data.Value);
                }

                //Check if keys are not null
                if (keys != null){
                    //Iterate the keys fields
                    foreach (KeyValuePair<String, String> data in substractFields(keys)){
                        //Add keys data intro the sql code
                        sql += String.Format("{0},", data.Value);
                    }
                }

                //Remove the last comma
                sql = sql.Remove(sql.Length - 1);
                //Close que sql table
                sql += ");";
            }
           
            return sql;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFactory
{
    public class SQLModels{

        public class SQLModel {

            //The basic data types in sqlite
            protected static string Integer() {
                return "integer";
            }

            protected static string Text() {
                return "text";
            }

            protected static string Varchar(int size = 50) {
                return string.Format("varchar({0})", size);
            }

            protected static string Blob() {
                return "blob";
            }

            protected static string Real() {
                return "real";
            }

            protected static string Numeric() {
                return "numeric";
            }

            //Static class for attributes
            protected static class Attributes{

                public static string Stick(string field, bool notNull = false, bool primaryKey = true, bool autoincrement = false, bool unique = false){
                    if (notNull == true){ field += " not null"; }
                    if (primaryKey == true) { field += " primary key"; }
                    if (autoincrement == true) { field += " autoincrement"; }
                    if (unique == true) { field += " unique"; }
                    return field;
                }
            }


        }

        //Class for construc the keys models
        public class Keys{

            protected static string ComposedPrimaryKey(string[] keyNames){
                //Declare a auxiliar field
                string keys = "";
                //Iterate the array with the key names
                foreach (string keyName in keyNames)
                {
                    //Construct an partial sql instruction with the keys
                    keys += string.Format("{0},", keyName);
                }
                //Remove the last comma ,
                keys = keys.Remove(keys.Length - 1);
                // return the sql instruction according with the 
                return String.Format("primary key({0})", keys);
            } 
            protected static string  ForeignKey(string foreignKeyInTable, string foreignKey, string connectToTable){
                return String.Format("foreign key({0}) references {1}({2})",
                          foreignKeyInTable, connectToTable, foreignKey);
            }

        }

    }
}

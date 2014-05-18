using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace SuperTrunfo
{
    class FileDataSource<T> : DataSource<T>
    {
        public DirectoryInfo folder;

        private List<String> props;

        private Type type;
		
		private String fileNameProperty;
		
		private String nameProperty;

        public FileDataSource(DirectoryInfo folder, String fileNameProperty)
        {
            this.folder = folder;
			this.fileNameProperty = fileNameProperty;
            type = typeof(T);
            readProperties();
			if(!folder.Exists){
				folder.Create();
			}
        }

        public List<T> getDataSource(){
            List<T> dataSource = new List<T>();

            FileInfo[] files = folder.GetFiles();

            foreach (FileInfo file in files)
            {

                using (StreamReader reader = new StreamReader(folder.FullName + Path.DirectorySeparatorChar + file.Name))
                {

                    T dataSourceObject = (T) Activator.CreateInstance(type);

                    int currentProperty = 0;
                    while (!reader.EndOfStream)
                    {
                        String line = reader.ReadLine();

                        Object value = getValue(type, props[currentProperty], line);

                        type.GetField(props[currentProperty]).SetValue(dataSourceObject, value);
                        currentProperty++;
                    }

                    dataSource.Add(dataSourceObject);
                }
                
            }
            return dataSource;
        }

        private Object getValue(Type type, String name, Object value){
            Type fieldType = type.GetField(name).FieldType;
            if (fieldType == typeof(String))
            {
                return value;
            }

            return fieldType.GetMethod("Parse", new[] { typeof(string) }).Invoke(null, new String[] { (String)value });
        }

        public bool setDataSource(List<T> data){
			
			deleteFiles();
			
			foreach(T obj in data){
				using (TextWriter writer = new StreamWriter(folder.FullName + Path.DirectorySeparatorChar + getFileName(obj), true))
                {
					props.ForEach((prop) => {
						String line = type.GetField(prop).GetValue (obj).ToString();
						writer.WriteLine(line);
					});
                }
			}
			
            return true;
        }
		
        public bool delete(T dataSourceItem){
            FileInfo[] files = folder.GetFiles();
			FileInfo toDelete = null;
			
			foreach(FileInfo file in files){
                if (getFileName(dataSourceItem).Equals(getFileName(dataSourceItem))){
					toDelete = file;
				}
			}
			
			if(toDelete != null) toDelete.Delete();
			return true;
		}
		
		private String getFileName(T obj){
			String nameValue = null;
			
			if(nameProperty == null && fileNameProperty != null){
				nameValue = type.GetField(fileNameProperty).GetValue(obj).ToString();
			}
			
			return nameValue == null ? obj.GetHashCode().ToString() : nameValue;
		}
		
		private void deleteFiles(){			

            FileInfo[] files = folder.GetFiles();
			
            foreach (FileInfo file in files){
				file.Delete();
			}
		}

        private void readProperties() {
            props = new List<string>();

            FieldInfo[] fields = type.GetFields();
         
            foreach(FieldInfo field in fields){
                props.Add(field.Name);
            }
        }
    }
}

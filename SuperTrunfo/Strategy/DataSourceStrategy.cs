using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace SuperTrunfo
{
	public class DataSourceStrategy<T> : Strategy<T>
	{
		
		public DataSourceStrategy ()
		{
			type = typeof(T);
			String name = type.Name;
			dataSource = new FileDataSource<T>(new DirectoryInfo("C:\\Users\\Maria\\Desktop\\test\\" + name + "\\"), "id");

            Container.set(dataSource);

			cachedDataSource = dataSource.getDataSource();
		}
		
		private Type type;
		
		private DataSource<T> dataSource;
		
		private List<T> cachedDataSource;
		
		public List<T> getAll(){
			return cachedDataSource;
		}
		
		public T getById(Object id){
			FieldInfo fieldId = type.GetField("id");
			
			return cachedDataSource.Find((listObject)=>{
				return fieldId.GetValue(listObject).Equals(id);
			});
			
		}

        public List<T> find(String propertyName, Object valueField){
            FieldInfo fieldId = type.GetField("id");
			return cachedDataSource.FindAll((listObject)=>{
                return fieldId.GetValue(listObject).Equals(valueField);
			});
		}
		
		public T findOne(String propertyName, Object valueField){
			return cachedDataSource.Find((listObject)=>{				
				return type.GetField(propertyName).GetValue(listObject).Equals(valueField);
			});
		}
		
		public Boolean delete(){
			return dataSource.setDataSource(new List<T>());
		}
		
		public Boolean delete(T item){
			return dataSource.delete(item);
		}
		
		public Boolean deleteOne(String propertyName, Object valueField){
			T toDelete = this.findOne(propertyName, valueField);
			
			if(toDelete != null){
				dataSource.delete(toDelete);
			}
            return true;
		}
	}
}


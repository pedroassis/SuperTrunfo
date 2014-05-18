using System;
using System.Collections.Generic;

namespace SuperTrunfo
{
	public interface Strategy<T>
	{		
		List<T> getAll();
		
		T getById(Object id);
		
		List<T> find(String propertyName, Object valueField);
		
		T findOne(String propertyName, Object valueField);
		
		Boolean delete();

        Boolean delete(T id);
		
		Boolean deleteOne(String propertyName, Object valueField);
	}
}


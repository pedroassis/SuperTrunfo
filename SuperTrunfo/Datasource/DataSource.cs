using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    interface DataSource
    {
        /// <summary>
        /// Should return a List of persisted objects
        /// </summary>
        /// <returns>List A new List of object of type T</returns>
        List<T> getDataSource<T>();

        /// <summary>
        /// Should persist the ListT to the specified dataSource type.
        /// </summary>
        /// <param name="dataSource">Object of type T to be persisted.</param>
        /// <returns>Returns true if persisted with success</returns>
        bool setDataSource<T>(List<T> dataSource);

        /// <summary>
        /// Should remove the T object from the specified dataSource type.
        /// </summary>
        /// <param name="DataSourceItem">Object of type T to be removed.</param>
        /// <returns>Returns true if removed with success</returns>
        bool delete<T>(T DataSourceItem);
    }
}

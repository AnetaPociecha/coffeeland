using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Coffeeland.Database.Records
{
    public interface IRecord
    {
        void Fill(DataRow dr);
    }
}

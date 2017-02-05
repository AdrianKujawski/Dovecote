using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Dovecote.Abstract {

	public interface ITable {
		long Id { get; set; }
		string Name { get; set; }
	}
}

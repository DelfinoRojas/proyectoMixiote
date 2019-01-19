using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMixiote
{
    class Folio
    {
        string FolioVenta, mesa, idEmpleado;
        int nPersonas, nCuentas;
        string fechaHoy,horaEntrada, horaSalida;

        public string FolioVenta1 { get => FolioVenta; set => FolioVenta = value; }
        public string Mesa { get => mesa; set => mesa = value; }
        public string IdEmpleado { get => idEmpleado; set => idEmpleado = value; }
        public int NPersonas { get => nPersonas; set => nPersonas = value; }
        public int NCuentas { get => nCuentas; set => nCuentas = value; }
        public string FechaHoy { get => fechaHoy; set => fechaHoy = value; }
        public string HoraEntrada { get => horaEntrada; set => horaEntrada = value; }
        public string HoraSalida { get => horaSalida; set => horaSalida = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews.OODesign
{
    public class ParkingLot
    {
        public List<IEnterable> EntryPoints;

        public List<IExitable> ExitPoints;

        private List<ParkingSpot> AllSpots;

        public List<ParkingSpot> AvailableSpots;
    }

    public class ParkingSpot
    {
        public bool isAvailable;
        public VehicleType Type;
    }

    public enum VehicleType
    {
        Motorcycle,
        Car,
        Bus
    }

    public class EntryKiosk : IEnterable
    {
        public Ticket IssueTicket()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class MultiPurposeKiosk: IEnterable, IExitable
    {
        public Ticket IssueTicket()
        {
            throw new NotImplementedException();
        }

        public bool AcceptTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public abstract float CalculateCost(Ticket ticket);
    }

    public class Ticket
    {
        public DateTime EntryTime;
        public DateTime ExitTIme;
    }

    public interface IEnterable
    {
        Ticket IssueTicket();
    }

    public interface IExitable
    {
        bool AcceptTicket(Ticket ticket);
    }
}

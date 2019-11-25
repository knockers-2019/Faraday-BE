using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaradayGrpcServer.Database
{
    public class Facade
    {
        private readonly DatabaseConnection _db;
        public Facade()
        {
            this._db = new DatabaseConnection();
        }

        //Locations
        public LocationModel GetLocationModel(LocationId locationId)
        {
            return _db.GetLocationModel(locationId);
        }

        public List<LocationModel> GetAllLocationModels()
        {
            return _db.GetAllLocationModels();
        }

        //Customers
        public CustomerModel GetCustomerModel(CustomerId customerId)
        {
            return _db.GetCustomerModel(customerId);
        }

        public CustomerModel GetCustomerModelByDriversLicense(CustomerDriversLicense driversLicense)
        {
            return _db.GetCustomerModelByDriversLicense(driversLicense);
        }

        public List<CustomerModel> GetAllCustomerModels()
        {
            return _db.GetAllCustomerModels();
        }

        public CustomerId CreateCustomerModel(CustomerModel customer)
        {
            return _db.CreateCustomerModel(customer);
        }

        //Cars
        public CarModel GetCarModel(CarId carId)
        {
            return _db.GetCarModel(carId);
        }

        public List<CarModel> GetAllAvailableCarModelsAtLocation(LocationModel location)
        {
            return _db.GetAllAvailableCarModelsAtLocation(location);
        }

        public List<CarModel> GetAllCarModels()
        {
            return _db.GetAllCarModels();
        }

        public CarId CreateCarModel(CarModel car)
        {
            return _db.CreateCarModel(car);
        }

        //Bookings
        public BookingModel GetBookingModel(BookingId bookingId)
        {
            return _db.GetBookingModel(bookingId);
        }

        public List<BookingModel> GetAllBookingModels()
        {
            return _db.GetAllBookingModels();
        }

        public List<BookingModel> GetAllActiveBookingModels()
        {
            return _db.GetAllActiveBookingModels();
        }

        public List<BookingModel> GetBookingModelByDriversLicense(CustomerModel customer)
        {
            return _db.GetBookingModelByDriversLicense(customer);
        }

        public BookingId CreateBookingModel(BookingModel booking)
        {
            return _db.CreateBookingModel(booking);
        }

        public AffectedRows CancelBookingModel(BookingModel booking)
        {
            return _db.CancelBookingModel(booking);
        }
    }
}

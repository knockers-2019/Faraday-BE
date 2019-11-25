using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaradayGrpcServer.Database;
using Grpc.Core;

namespace FaradayGrpcServer.Services
{
    public class BookingService : Bookings.BookingsBase
    {
        private readonly Facade _facade;
        public BookingService()
        {
            this._facade = new Facade();
        }
        //Location
        public override async Task GetAllLocationModels(EmptyRequest request, IServerStreamWriter<LocationModel> responseStream, ServerCallContext context)
        {
            List<LocationModel> locations = _facade.GetAllLocationModels();

            foreach (var l in locations)
            {
                await responseStream.WriteAsync(l);
            }
        }

        public override Task<LocationModel> GetLocationModel(LocationId request, ServerCallContext context)
        {
            return Task.FromResult(_facade.GetLocationModel(request));
        }
        //Customers
        public override Task<CustomerId> CreateCustomerModel(CustomerModel request, ServerCallContext context)
        {
            return Task.FromResult(_facade.CreateCustomerModel(request));
        }

        public override async Task GetAllCustomerModels(EmptyRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = _facade.GetAllCustomerModels();

            foreach (CustomerModel customer in customers)
            {
                await responseStream.WriteAsync(customer);
            }

        }

        public override Task<CustomerModel> GetCustomerModel(CustomerId request, ServerCallContext context)
        {
            return Task.FromResult(_facade.GetCustomerModel(request));
        }

        public override Task<CustomerModel> GetCustomerModelByDriversLicense(CustomerDriversLicense request, ServerCallContext context)
        {
            return Task.FromResult(_facade.GetCustomerModelByDriversLicense(request));
        }

        //Cars
        public override async Task GetAllCarModels(EmptyRequest request, IServerStreamWriter<CarModel> responseStream, ServerCallContext context)
        {
            List<CarModel> cars = _facade.GetAllCarModels();

            foreach (CarModel car in cars)
            {
                await responseStream.WriteAsync(car);
            }
        }

        public override async Task GetAllAvailableCarModelsAtLocation(LocationModel request, IServerStreamWriter<CarModel> responseStream, ServerCallContext context)
        {
            List<CarModel> cars = _facade.GetAllAvailableCarModelsAtLocation(request);

            foreach (CarModel car in cars)
            {
                await responseStream.WriteAsync(car);
            }
        }

        public override Task<CarModel> GetCarModel(CarId request, ServerCallContext context)
        {
            return Task.FromResult(_facade.GetCarModel(request));
        }

        public override Task<CarId> CreateCarModel(CarModel request, ServerCallContext context)
        {
            return Task.FromResult(_facade.CreateCarModel(request));
        }

        //Bookings
        public override Task<BookingId> CreateBookingModel(BookingModel request, ServerCallContext context)
        {
            return Task.FromResult(_facade.CreateBookingModel(request));
        }

        public override Task<BookingModel> GetBookingModel(BookingId request, ServerCallContext context)
        {
            return Task.FromResult(_facade.GetBookingModel(request));
        }

        public override async Task GetAllBookingModels(EmptyRequest request, IServerStreamWriter<BookingModel> responseStream, ServerCallContext context)
        {
            List<BookingModel> bookings = _facade.GetAllBookingModels();

            foreach (BookingModel booking in bookings)
            {
                await responseStream.WriteAsync(booking);
            }
        }

        public override async Task GetAllActiveBookings(EmptyRequest request, IServerStreamWriter<BookingModel> responseStream, ServerCallContext context)
        {
            List<BookingModel> bookings = _facade.GetAllActiveBookingModels();

            foreach (BookingModel booking in bookings)
            {
                await responseStream.WriteAsync(booking);
            }
        }

        public override async Task GetCustomerBookingModels(CustomerModel request, IServerStreamWriter<BookingModel> responseStream, ServerCallContext context)
        {
            List<BookingModel> bookings = _facade.GetBookingModelByDriversLicense(request);
            foreach (BookingModel booking in bookings)
            {
                await responseStream.WriteAsync(booking);
            }
        }

        public override Task<AffectedRows> CancelBookingModel(BookingModel request, ServerCallContext context)
        {
            return Task.FromResult(_facade.CancelBookingModel(request));
        }
    }
}

using FaradayGrpcServer;
using Grpc.Net.Client;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace FaradayBEClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Setting up the channel
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var bookingClient = new Bookings.BookingsClient(channel);

            //Helpers
            Random rng = new Random();
            EmptyRequest emptyRequest = new EmptyRequest();

            //Customers
            CustomerModel customer = new CustomerModel();
            CustomerId customerRequest = new CustomerId { Id = rng.Next(101) };

            Console.WriteLine("TESTING CUSTOMER METHODS\n------------------------------------");
            Console.WriteLine("ALL CUSTOMERS\n------------------------------------");
            using (var requestAllCustomers = bookingClient.GetAllCustomerModels(emptyRequest))
            {

                while (await requestAllCustomers.ResponseStream.MoveNext())
                {
                    customer = requestAllCustomers.ResponseStream.Current;
                    Console.WriteLine($"ID: {customer.Id}, FN: {customer.FirstName}, LN: {customer.LastName}, G: {customer.Gender}, DL: {customer.DriversLicense}");
                }
            }
            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();
            Console.WriteLine("GET CUSTOMER (CustomerId customerId) - Random id between 0-100\n------------------------------------");
            customer = await bookingClient.GetCustomerModelAsync(customerRequest);
            Console.Write($"ID: {customer.Id}, FN: {customer.FirstName}, LN: {customer.LastName}, SEX: {customer.Gender}, DL: {customer.DriversLicense}");

            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();
            Console.WriteLine("GET CUSTOMER (DriversLicense driversLicense) - Drivers license from the recently selected Customer\n------------------------------------");
            customer = await bookingClient.GetCustomerModelByDriversLicenseAsync(new CustomerDriversLicense { DriversLicense = customer.DriversLicense });
            Console.Write($"ID: {customer.Id}, FN: {customer.FirstName}, LN: {customer.LastName}, SEX: {customer.Gender}, DL: {customer.DriversLicense}");

            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();
            Console.WriteLine("CREATE CUSTOMER (CustomerModel customer)\n------------------------------------");
            CustomerModel newCustomerRequest = new CustomerModel { FirstName = "Test", LastName = "Customer", Gender = "Male", DriversLicense = "DK-2730" };
            CustomerId newCustomerId = new CustomerId();
            newCustomerId = await bookingClient.CreateCustomerModelAsync(newCustomerRequest);
            Console.WriteLine($"Customer Created! The customer was assigned ID: {newCustomerId.Id}");

            //Location
            LocationModel location = new LocationModel();
            LocationId locationRequest = new LocationId { Id = rng.Next(8) };

            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();

            Console.WriteLine("TESTING LOCATION METHODS\n------------------------------------");
            Console.WriteLine("ALL LOCATIONS\n------------------------------------");
            using (var requestAllLocations = bookingClient.GetAllLocationModels(emptyRequest))
            {
                while (await requestAllLocations.ResponseStream.MoveNext())
                {
                    location = requestAllLocations.ResponseStream.Current;
                    Console.WriteLine($"ID: {location.Id}, A: {location.Address}, CI: {location.City}, Z: {location.Zipcode}, CO: {location.Country}");
                }
            }
            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();

            Console.WriteLine("GET LOCATION (LocationId locationId) - Random id between 0-7\n------------------------------------");
            customer = await bookingClient.GetCustomerModelAsync(customerRequest);
            Console.Write($"ID: {location.Id}, A: {location.Address}, CI: {location.City}, Z: {location.Zipcode}, CO: {location.Country}");

            //Cars
            CarModel car = new CarModel();
            CarId carRequest = new CarId { Id = rng.Next(101) };

            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();

            Console.WriteLine("TESTING CAR METHODS\n------------------------------------");
            Console.WriteLine("ALL CARS\n------------------------------------");
            using (var requestlAllCars = bookingClient.GetAllCarModels(emptyRequest))
            {
                while (await requestlAllCars.ResponseStream.MoveNext())
                {
                    car = requestlAllCars.ResponseStream.Current;
                    Console.WriteLine($"ID: {car.Id}, CL-ID: {car.Location.Id}, CL-A: {car.Location.Address}, CL-CI: {car.Location.City}, CL-Z: {car.Location.Zipcode}, CL-CO: {car.Location.Country}," +
                        $" P: {car.Price}, B: {car.Brand}, M: {car.Model}, C: {car.Color}, D: {car.Doors}, AA: {car.AnimalsAllowed}, A: {car.Available}, CT: {car.CarType}, FT: {car.FuelType}, LP: {car.LicensePlate}");
                }
            }

            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();

            Console.WriteLine($"ALL AVAILABLE CARS AT ID: {location.Id} A: {location.Address} C: {location.City} Z: {location.Zipcode} CO: {location.Country}\n------------------------------------");
            using (var requestAllAvailableCarsAtLocation = bookingClient.GetAllAvailableCarModelsAtLocation(location))
            {
                while (await requestAllAvailableCarsAtLocation.ResponseStream.MoveNext())
                {
                    car = requestAllAvailableCarsAtLocation.ResponseStream.Current;
                    Console.WriteLine($"ID: {car.Id}, CL-ID: {car.Location.Id}, CL-A: {car.Location.Address}, CL-CI: {car.Location.City}, CL-Z: {car.Location.Zipcode}, CL-CO: {car.Location.Country}," +
                        $" P: {car.Price}, B: {car.Brand}, M: {car.Model}, C: {car.Color}, D: {car.Doors}, AA: {car.AnimalsAllowed}, A: {car.Available}, CT: {car.CarType}, FT: {car.FuelType}, LP: {car.LicensePlate}");
                }
            }
            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();

            Console.WriteLine("GET CAR (CarId carId) - Random id between 0-100\n------------------------------------");
            car = await bookingClient.GetCarModelAsync(carRequest);
            Console.WriteLine($"ID: {car.Id}, CL-ID: {car.Location.Id}, CL-A: {car.Location.Address}, CL-CI: {car.Location.City}, CL-Z: {car.Location.Zipcode}, CL-CO: {car.Location.Country}," +
                        $" P: {car.Price}, B: {car.Brand}, M: {car.Model}, C: {car.Color}, D: {car.Doors}, AA: {car.AnimalsAllowed}, A: {car.Available}, CT: {car.CarType}, FT: {car.FuelType}, LP: {car.LicensePlate}");

            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();
            Console.WriteLine("CREATE CAR (CarModel car)\n------------------------------------");
            CarModel newCarRequest = new CarModel { Location = location, Doors = 3, Brand = "Test", Model = "To Be Deleted", CarType = "TBD", FuelType = "IOT", Color = "Black", Price = 2000.0, AnimalsAllowed = false, Available = true, LicensePlate = "TEST-DK" };
            CarId newCarId = await bookingClient.CreateCarModelAsync(newCarRequest);
            Console.WriteLine($"Car Created! The car was assigned ID: {newCarId.Id}");

            //Bookings
            BookingModel booking = new BookingModel();

            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();

            Console.WriteLine("TESTING BOOKING METHODS\n------------------------------------");
            Console.WriteLine("ALL BOOKINGS\n------------------------------------");
            using (var requestAllBookings = bookingClient.GetAllBookingModels(emptyRequest))
            {
                while (await requestAllBookings.ResponseStream.MoveNext())
                {
                    booking = requestAllBookings.ResponseStream.Current;
                    Console.WriteLine($"ID: {booking.Id}, PUL-ID: {booking.PickupLocation.Id}, PUL-A: {booking.PickupLocation.Address}, PUL-CI: {booking.PickupLocation.City}, PUL-Z: {booking.PickupLocation.Zipcode}, PUL-CO: {booking.PickupLocation.Country}," +
                        $" DOL-ID: {booking.DropoffLocation.Id}, DOL-A: {booking.DropoffLocation.Address}, DOL-CI: {booking.DropoffLocation.City}, DOL-Z: {booking.DropoffLocation.Zipcode}, DOL-CO: {booking.DropoffLocation.Country}," +
                        $" C-ID: {booking.Car.Id}, CL-ID: {booking.Car.Location.Id}, CL-A: {booking.Car.Location.Address}, CL-CI: {booking.Car.Location.City}, CL-Z: {booking.Car.Location.Zipcode}, CL-CO: {booking.Car.Location.Country}," +
                        $" C-P: {booking.Car.Price}, C-B: {booking.Car.Brand}, C-M: {booking.Car.Model}, C-C: {booking.Car.Color}, C-D: {booking.Car.Doors}, C-AA: {booking.Car.AnimalsAllowed}, C-A: {booking.Car.Available}, CT: {booking.Car.CarType}, C-FT: {booking.Car.FuelType}, C-LP: {booking.Car.LicensePlate}," +
                        $" CUST-ID: {booking.Customer.Id}, CUST-FN: {booking.Customer.FirstName}, CUST-LN: {booking.Customer.LastName}, CUST-SEX: {booking.Customer.Gender}, CUST-DL: {booking.Customer.DriversLicense}" +
                        $" PUD: {booking.PickupDate}, DOD: {booking.DropoffDate}, IC: {booking.IsCancelled}");
                }
            }
            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();

            using (var requestAllActiveBookings = bookingClient.GetAllActiveBookings(emptyRequest))
            {
                while (await requestAllActiveBookings.ResponseStream.MoveNext())
                {
                    booking = requestAllActiveBookings.ResponseStream.Current;
                    Console.WriteLine($"ID: {booking.Id}, PUL-ID: {booking.PickupLocation.Id}, PUL-A: {booking.PickupLocation.Address}, PUL-CI: {booking.PickupLocation.City}, PUL-Z: {booking.PickupLocation.Zipcode}, PUL-CO: {booking.PickupLocation.Country}," +
                        $" DOL-ID: {booking.DropoffLocation.Id}, DOL-A: {booking.DropoffLocation.Address}, DOL-CI: {booking.DropoffLocation.City}, DOL-Z: {booking.DropoffLocation.Zipcode}, DOL-CO: {booking.DropoffLocation.Country}," +
                        $" C-ID: {booking.Car.Id}, CL-ID: {booking.Car.Location.Id}, CL-A: {booking.Car.Location.Address}, CL-CI: {booking.Car.Location.City}, CL-Z: {booking.Car.Location.Zipcode}, CL-CO: {booking.Car.Location.Country}," +
                        $" C-P: {booking.Car.Price}, C-B: {booking.Car.Brand}, C-M: {booking.Car.Model}, C-C: {booking.Car.Color}, C-D: {booking.Car.Doors}, C-AA: {booking.Car.AnimalsAllowed}, C-A: {booking.Car.Available}, CT: {booking.Car.CarType}, C-FT: {booking.Car.FuelType}, C-LP: {booking.Car.LicensePlate}," +
                        $" CUST-ID: {booking.Customer.Id}, CUST-FN: {booking.Customer.FirstName}, CUST-LN: {booking.Customer.LastName}, CUST-SEX: {booking.Customer.Gender}, CUST-DL: {booking.Customer.DriversLicense}" +
                        $" PUD: {booking.PickupDate}, DOD: {booking.DropoffDate}, IC: {booking.IsCancelled}");
                }
            }
            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();

            Console.WriteLine("CREATE BOOKING (BookingModel booking)\n------------------------------------");
            BookingModel newBookingRequest = new BookingModel { PickupLocation = location, DropoffLocation = location, Car = car, Customer = customer, PickupDate = "TE/ST/TEST", DropoffDate = "TE/ST/TEST", IsCancelled = false };
            BookingId newBookingId = await bookingClient.CreateBookingModelAsync(newBookingRequest);
            Console.WriteLine($"Booking Created! The booking was assigned ID: {newBookingId.Id}");

            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();

            Console.WriteLine($"GET BOOKING (BookingId bookingId) - Use id ({newBookingId.Id}), from the newly created booking\n------------------------------------");
            booking = await bookingClient.GetBookingModelAsync(newBookingId);
            Console.WriteLine($"ID: {booking.Id}, PUL-ID: {booking.PickupLocation.Id}, PUL-A: {booking.PickupLocation.Address}, PUL-CI: {booking.PickupLocation.City}, PUL-Z: {booking.PickupLocation.Zipcode}, PUL-CO: {booking.PickupLocation.Country}," +
                        $" DOL-ID: {booking.DropoffLocation.Id}, DOL-A: {booking.DropoffLocation.Address}, DOL-CI: {booking.DropoffLocation.City}, DOL-Z: {booking.DropoffLocation.Zipcode}, DOL-CO: {booking.DropoffLocation.Country}," +
                        $" C-ID: {booking.Car.Id}, CL-ID: {booking.Car.Location.Id}, CL-A: {booking.Car.Location.Address}, CL-CI: {booking.Car.Location.City}, CL-Z: {booking.Car.Location.Zipcode}, CL-CO: {booking.Car.Location.Country}," +
                        $" C-P: {booking.Car.Price}, C-B: {booking.Car.Brand}, C-M: {booking.Car.Model}, C-C: {booking.Car.Color}, C-D: {booking.Car.Doors}, C-AA: {booking.Car.AnimalsAllowed}, C-A: {booking.Car.Available}, CT: {booking.Car.CarType}, C-FT: {booking.Car.FuelType}, C-LP: {booking.Car.LicensePlate}," +
                        $" CUST-ID: {booking.Customer.Id}, CUST-FN: {booking.Customer.FirstName}, CUST-LN: {booking.Customer.LastName}, CUST-SEX: {booking.Customer.Gender}, CUST-DL: {booking.Customer.DriversLicense}" +
                        $" PUD: {booking.PickupDate}, DOD: {booking.DropoffDate}, IC: {booking.IsCancelled}");

            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();

            Console.WriteLine($"GET BOOKING (CustomerModel customer) - Use the customer ({booking.Customer.Id}) from the newly created booking.\n------------------------------------");
            using (var requestCustomerBookings = bookingClient.GetCustomerBookingModels(booking.Customer))
            {
                while (await requestCustomerBookings.ResponseStream.MoveNext())
                {
                    booking = requestCustomerBookings.ResponseStream.Current;
                    Console.WriteLine($"ID: {booking.Id}, PUL-ID: {booking.PickupLocation.Id}, PUL-A: {booking.PickupLocation.Address}, PUL-CI: {booking.PickupLocation.City}, PUL-Z: {booking.PickupLocation.Zipcode}, PUL-CO: {booking.PickupLocation.Country}," +
                        $" DOL-ID: {booking.DropoffLocation.Id}, DOL-A: {booking.DropoffLocation.Address}, DOL-CI: {booking.DropoffLocation.City}, DOL-Z: {booking.DropoffLocation.Zipcode}, DOL-CO: {booking.DropoffLocation.Country}," +
                        $" C-ID: {booking.Car.Id}, CL-ID: {booking.Car.Location.Id}, CL-A: {booking.Car.Location.Address}, CL-CI: {booking.Car.Location.City}, CL-Z: {booking.Car.Location.Zipcode}, CL-CO: {booking.Car.Location.Country}," +
                        $" C-P: {booking.Car.Price}, C-B: {booking.Car.Brand}, C-M: {booking.Car.Model}, C-C: {booking.Car.Color}, C-D: {booking.Car.Doors}, C-AA: {booking.Car.AnimalsAllowed}, C-A: {booking.Car.Available}, CT: {booking.Car.CarType}, C-FT: {booking.Car.FuelType}, C-LP: {booking.Car.LicensePlate}," +
                        $" CUST-ID: {booking.Customer.Id}, CUST-FN: {booking.Customer.FirstName}, CUST-LN: {booking.Customer.LastName}, CUST-SEX: {booking.Customer.Gender}, CUST-DL: {booking.Customer.DriversLicense}" +
                        $" PUD: {booking.PickupDate}, DOD: {booking.DropoffDate}, IC: {booking.IsCancelled}");
                }
            }

            Console.WriteLine("\n------------------------------------\nPRESS ENTER TO START NEXT TEST");
            Console.ReadLine();

            Console.WriteLine("CANCEL BOOKING (BookingModel booking) - Cancel the newly created booking.\n------------------------------------");
            AffectedRows affectedRows = await bookingClient.CancelBookingModelAsync(booking);
            if (affectedRows.AffectedRows_ > 0)
            {
                Console.WriteLine($"Booking cancelled! {affectedRows.AffectedRows_} row(s) affected");
            }
            else
            {
                Console.WriteLine("Cancel booking failed!");
            }

            Console.WriteLine("\n------------------------------------\nTEST ENDED PRESS ENTER TO TERMINATE THE PROGRAM");
            Console.ReadLine();

        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaradayGrpcServer.Database
{
    public class DatabaseConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DatabaseConnection()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "mysql50.unoeuro.com";
            database = "mikkelertbjerg_dk_db_ls";
            uid = "mikkelertbjerg_dk";
            password = "knockers2019";
            string connectionString;
            connectionString = $"Server={server};Database={database};Uid={uid};Pwd={password};";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private void OpenConnection()
        {
            connection.Open();
        }

        //Close connection
        private void CloseConnection()
        {
            connection.Close();
        }

        //Locations
        public LocationModel GetLocationModel(LocationId locationId)
        {
            string query = $"SELECT * FROM Locations WHERE id = '{locationId.Id}';";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                this.OpenConnection();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            LocationModel location = new LocationModel();
            while (dataReader.Read())
            {
                location.Id = dataReader.GetInt32(0);
                location.Address = dataReader.GetString(1);
                location.City = dataReader.GetString(2);
                location.Zipcode = dataReader.GetString(3);
                location.Country = dataReader.GetString(4);
            };
            dataReader.Close();
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                this.CloseConnection();
            }
            return location;
        }

        public List<LocationModel> GetAllLocationModels()
        {
            string query = $"SELECT * FROM Locations;";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                this.OpenConnection();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<LocationModel> locations = new List<LocationModel>();
            while (dataReader.Read())
            {
                locations.Add(new LocationModel
                {
                    Id = dataReader.GetInt32(0),
                    Address = dataReader.GetString(1),
                    City = dataReader.GetString(2),
                    Zipcode = dataReader.GetString(3),
                    Country = dataReader.GetString(4),
                });

            };
            dataReader.Close();
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                this.CloseConnection();
            }
            return locations;
        }

        //Customers
        public CustomerModel GetCustomerModel(CustomerId customerId)
        {
            string query = $"SELECT * FROM Customers WHERE id = '{customerId.Id}';";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                this.OpenConnection();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            CustomerModel customer = new CustomerModel();
            while (dataReader.Read())
            {
                customer.Id = dataReader.GetInt32(0);
                customer.FirstName = dataReader.GetString(1);
                customer.LastName = dataReader.GetString(2);
                customer.Gender = dataReader.GetString(3);
                customer.DriversLicense = dataReader.GetString(4);
            };
            dataReader.Close();
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                this.CloseConnection();
            }
            return customer;
        }

        public List<CustomerModel> GetAllCustomerModels()
        {
            string query = $"SELECT * FROM Customers;";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                this.OpenConnection();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<CustomerModel> customers = new List<CustomerModel>();
            while (dataReader.Read())
            {
                customers.Add(new CustomerModel
                {
                    Id = dataReader.GetInt32(0),
                    FirstName = dataReader.GetString(1),
                    LastName = dataReader.GetString(2),
                    Gender = dataReader.GetString(3),
                    DriversLicense = dataReader.GetString(4),
                });
            };
            dataReader.Close();
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                this.CloseConnection();
            }
            return customers;
        }

        public CustomerId CreateCustomerModel(CustomerModel customer)
        {
            string query = $"INSERT INTO Customers (first_name, last_name, gender, drivers_license)" +
                $"VALUES ('{customer.FirstName}', '{customer.LastName}', '{customer.Gender}', '{customer.DriversLicense}');";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                this.OpenConnection();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            CustomerId customerId = new CustomerId();
            customerId.Id = Convert.ToInt32(cmd.LastInsertedId);
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                this.CloseConnection();
            }
            return customerId;
        }

        //Cars
        public CarModel GetCarModel(CarId carId)
        {
            string query = $"SELECT * FROM Cars c INNER JOIN Locations l ON c.fk_location_id = l.id WHERE c.id = '{carId.Id}';";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                this.OpenConnection();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            CarModel car = new CarModel();
            LocationModel location = new LocationModel();
            while (dataReader.Read())
            {
                car.Id = dataReader.GetInt32(0);
                //Skip "1" as that's the location id, which we get twice. Could optimize SQL slightly to avoid that.
                car.Price = dataReader.GetDouble(2);
                car.Brand = dataReader.GetString(3);
                car.Model = dataReader.GetString(4);
                car.Color = dataReader.GetString(5);
                car.Doors = dataReader.GetInt32(6);
                car.AnimalsAllowed = dataReader.GetBoolean(7);
                car.Available = dataReader.GetBoolean(8);
                car.CarType = dataReader.GetString(9);
                car.FuelType = dataReader.GetString(10);
                car.LicensePlate = dataReader.GetString(11);
                location.Id = dataReader.GetInt32(12);
                location.Address = dataReader.GetString(13);
                location.City = dataReader.GetString(14);
                location.Zipcode = dataReader.GetString(15);
                location.Country = dataReader.GetString(16);
                car.Location = location;
            };
            dataReader.Close();
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                this.CloseConnection();
            }
            return car;
        }

        public List<CarModel> GetAllCarModels()
        {
            string query = $"SELECT * FROM Cars c INNER JOIN Locations l ON c.fk_location_id = l.id; ";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                this.OpenConnection();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<CarModel> cars = new List<CarModel>();
            while (dataReader.Read())
            {
                cars.Add(new CarModel
                {
                    Id = dataReader.GetInt32(0),
                    Location = new LocationModel
                    {
                        Id = dataReader.GetInt32(12),
                        Address = dataReader.GetString(13),
                        City = dataReader.GetString(14),
                        Zipcode = dataReader.GetString(15),
                        Country = dataReader.GetString(16),
                    },
                    Price = dataReader.GetDouble(2),
                    Brand = dataReader.GetString(3),
                    Model = dataReader.GetString(4),
                    Color = dataReader.GetString(5),
                    Doors = dataReader.GetInt32(6),
                    AnimalsAllowed = dataReader.GetBoolean(7),
                    Available = dataReader.GetBoolean(8),
                    CarType = dataReader.GetString(9),
                    FuelType = dataReader.GetString(10),
                    LicensePlate = dataReader.GetString(11)
                });
            };
            dataReader.Close();
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                this.CloseConnection();
            }
            return cars;
        }

        public CarId CreateCarModel(CarModel car)
        {
            //Need to fix the bool values for the car object as they are getting passed as false regardless (TinyInt is 0 = false and 1 = true). BUt the Available and AA props are true/false.
            string query = $"INSERT INTO Cars (fk_location_id, price, brand, model, color, doors, animals_allowed, available, car_type, fuel_type, license_plate)" +
                $"VALUES ('{car.Location.Id}', '{car.Price}', '{car.Brand}', '{car.Model}', '{car.Color}', '{car.Doors}', '{car.AnimalsAllowed}', '{car.Available}', '{car.CarType}', '{car.FuelType}', '{car.LicensePlate}');";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                this.OpenConnection();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            CarId carId = new CarId();
            carId.Id = Convert.ToInt32(cmd.LastInsertedId);
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                this.CloseConnection();
            }
            return carId;
        }

        //Bookings
        //MySQL Selector
        //SELECT b.id, b.pickup_date, b.dropoff_date, b.is_cancelled, locp.id, locp.address, locp.city, locp.zipcode, locp.country, locd.id, locd.address, locd.city, locd.zipcode, locd.country, car.id, locc.id, locc.address, locc.city, locc.zipcode, locc.country, car.price, car.brand, car.model, car.color, car.doors, car.animals_allowed, car.available, car.car_type, car.fuel_type, car.license_plate, cust.id, cust.first_name, cust.last_name, cust.gender, cust.drivers_license FROM Bookings b INNER JOIN Locations locp ON b.fk_pickup_location_id = locp.id INNER JOIN Locations locd ON b.fk_dropoff_location_id = locd.id INNER JOIN Cars car ON b.fk_car_id = car.id INNER JOIN Locations locc ON car.fk_location_id = locc.id INNER JOIN Customers cust ON b.fk_customer_id = cust.id WHERE b.id = 1
        public BookingModel GetBookingModel(BookingId bookingId)
        {
            string query = $"SELECT " +
            $"b.id, b.pickup_date, b.dropoff_date, b.is_cancelled, " +
            $"locp.id, locp.address, locp.city, locp.zipcode, locp.country, " +
            $"locd.id, locd.address, locd.city, locd.zipcode, locd.country, " +
            $"car.id, locc.id, locc.address, locc.city, locc.zipcode, locc.country, " +
            $"car.price, car.brand, car.model, car.color, car.doors, " +
            $"car.animals_allowed, car.available, car.car_type, car.fuel_type, car.license_plate, " +
            $"cust.id, cust.first_name, cust.last_name, cust.gender, cust.drivers_license " +
            $"FROM Bookings b " +
            $"INNER JOIN Locations locp ON b.fk_pickup_location_id = locp.id " +
            $"INNER JOIN Locations locd ON b.fk_dropoff_location_id = locd.id " +
            $"INNER JOIN Cars car ON b.fk_car_id = car.id " +
            $"INNER JOIN Locations locc ON car.fk_location_id = locc.id " +
            $"INNER JOIN Customers cust ON b.fk_customer_id = cust.id " +
            $"WHERE b.id = '{bookingId.Id}';";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                this.OpenConnection();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            BookingModel booking = new BookingModel();
            while (dataReader.Read())
            {
                booking.Id = dataReader.GetInt32(0);
                booking.PickupDate = dataReader.GetString(1);
                booking.DropoffDate = dataReader.GetString(2);
                booking.IsCancelled = dataReader.GetBoolean(3);
                booking.PickupLocation = new LocationModel
                {
                    Id = dataReader.GetInt32(4),
                    Address = dataReader.GetString(5),
                    City = dataReader.GetString(6),
                    Zipcode = dataReader.GetString(7),
                    Country = dataReader.GetString(8)
                };
                booking.DropoffLocation = new LocationModel
                {
                    Id = dataReader.GetInt32(9),
                    Address = dataReader.GetString(10),
                    City = dataReader.GetString(11),
                    Zipcode = dataReader.GetString(12),
                    Country = dataReader.GetString(13)
                };
                booking.Car = new CarModel
                {
                    Id = dataReader.GetInt32(14),
                    Location = new LocationModel
                    {
                        Id = dataReader.GetInt32(15),
                        Address = dataReader.GetString(16),
                        City = dataReader.GetString(17),
                        Zipcode = dataReader.GetString(18),
                        Country = dataReader.GetString(19)
                    },
                    Price = dataReader.GetDouble(20),
                    Brand = dataReader.GetString(21),
                    Model = dataReader.GetString(22),
                    Color = dataReader.GetString(23),
                    Doors = dataReader.GetInt32(24),
                    AnimalsAllowed = dataReader.GetBoolean(25),
                    Available = dataReader.GetBoolean(26),
                    CarType = dataReader.GetString(27),
                    FuelType = dataReader.GetString(28),
                    LicensePlate = dataReader.GetString(29)
                };
                booking.Customer = new CustomerModel
                {
                    Id = dataReader.GetInt32(30),
                    FirstName = dataReader.GetString(31),
                    LastName = dataReader.GetString(32),
                    Gender = dataReader.GetString(33),
                    DriversLicense = dataReader.GetString(34)
                };
            };
            dataReader.Close();
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                this.CloseConnection();
            }
            return booking;
        }

        public List<BookingModel> GetBookingModelByDriversLicense(CustomerModel customer)
        {
            string query = $"SELECT " +
            $"b.id, b.pickup_date, b.dropoff_date, b.is_cancelled, " +
            $"locp.id, locp.address, locp.city, locp.zipcode, locp.country, " +
            $"locd.id, locd.address, locd.city, locd.zipcode, locd.country, " +
            $"car.id, locc.id, locc.address, locc.city, locc.zipcode, locc.country, " +
            $"car.price, car.brand, car.model, car.color, car.doors, " +
            $"car.animals_allowed, car.available, car.car_type, car.fuel_type, car.license_plate, " +
            $"cust.id, cust.first_name, cust.last_name, cust.gender, cust.drivers_license " +
            $"FROM Bookings b " +
            $"INNER JOIN Locations locp ON b.fk_pickup_location_id = locp.id " +
            $"INNER JOIN Locations locd ON b.fk_dropoff_location_id = locd.id " +
            $"INNER JOIN Cars car ON b.fk_car_id = car.id " +
            $"INNER JOIN Locations locc ON car.fk_location_id = locc.id " +
            $"INNER JOIN Customers cust ON b.fk_customer_id = cust.id " +
            $"WHERE cust.drivers_license = '{customer.DriversLicense}';";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                this.OpenConnection();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<BookingModel> bookings = new List<BookingModel>();
            while (dataReader.Read())
            {
                bookings.Add(new BookingModel
                {
                    Id = dataReader.GetInt32(0),
                    PickupDate = dataReader.GetString(1),
                    DropoffDate = dataReader.GetString(2),
                    IsCancelled = dataReader.GetBoolean(3),
                    PickupLocation = new LocationModel
                    {
                        Id = dataReader.GetInt32(4),
                        Address = dataReader.GetString(5),
                        City = dataReader.GetString(6),
                        Zipcode = dataReader.GetString(7),
                        Country = dataReader.GetString(8)
                    },
                    DropoffLocation = new LocationModel
                    {
                        Id = dataReader.GetInt32(9),
                        Address = dataReader.GetString(10),
                        City = dataReader.GetString(11),
                        Zipcode = dataReader.GetString(12),
                        Country = dataReader.GetString(13)
                    },
                    Car = new CarModel
                    {
                        Id = dataReader.GetInt32(14),
                        Location = new LocationModel
                        {
                            Id = dataReader.GetInt32(15),
                            Address = dataReader.GetString(16),
                            City = dataReader.GetString(17),
                            Zipcode = dataReader.GetString(18),
                            Country = dataReader.GetString(19)
                        },
                        Price = dataReader.GetDouble(20),
                        Brand = dataReader.GetString(21),
                        Model = dataReader.GetString(22),
                        Color = dataReader.GetString(23),
                        Doors = dataReader.GetInt32(24),
                        AnimalsAllowed = dataReader.GetBoolean(25),
                        Available = dataReader.GetBoolean(26),
                        CarType = dataReader.GetString(27),
                        FuelType = dataReader.GetString(28),
                        LicensePlate = dataReader.GetString(29)
                    },
                    Customer = new CustomerModel
                    {
                        Id = dataReader.GetInt32(30),
                        FirstName = dataReader.GetString(31),
                        LastName = dataReader.GetString(32),
                        Gender = dataReader.GetString(33),
                        DriversLicense = dataReader.GetString(34)
                    }
                });
            }
            dataReader.Close();
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                this.CloseConnection();
            }
            return bookings;
        }

        public List<BookingModel> GetAllBookingModels()
        {
            string query = $"SELECT " +
            $"b.id, b.pickup_date, b.dropoff_date, b.is_cancelled, " +
            $"locp.id, locp.address, locp.city, locp.zipcode, locp.country, " +
            $"locd.id, locd.address, locd.city, locd.zipcode, locd.country, " +
            $"car.id, locc.id, locc.address, locc.city, locc.zipcode, locc.country, " +
            $"car.price, car.brand, car.model, car.color, car.doors, " +
            $"car.animals_allowed, car.available, car.car_type, car.fuel_type, car.license_plate, " +
            $"cust.id, cust.first_name, cust.last_name, cust.gender, cust.drivers_license " +
            $"FROM Bookings b " +
            $"INNER JOIN Locations locp ON b.fk_pickup_location_id = locp.id " +
            $"INNER JOIN Locations locd ON b.fk_dropoff_location_id = locd.id " +
            $"INNER JOIN Cars car ON b.fk_car_id = car.id " +
            $"INNER JOIN Locations locc ON car.fk_location_id = locc.id " +
            $"INNER JOIN Customers cust ON b.fk_customer_id = cust.id;";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                this.OpenConnection();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<BookingModel> bookings = new List<BookingModel>();
            while (dataReader.Read())
            {
                bookings.Add(new BookingModel
                {
                    Id = dataReader.GetInt32(0),
                    PickupDate = dataReader.GetString(1),
                    DropoffDate = dataReader.GetString(2),
                    IsCancelled = dataReader.GetBoolean(3),
                    PickupLocation = new LocationModel
                    {
                        Id = dataReader.GetInt32(4),
                        Address = dataReader.GetString(5),
                        City = dataReader.GetString(6),
                        Zipcode = dataReader.GetString(7),
                        Country = dataReader.GetString(8)
                    },
                    DropoffLocation = new LocationModel
                    {
                        Id = dataReader.GetInt32(9),
                        Address = dataReader.GetString(10),
                        City = dataReader.GetString(11),
                        Zipcode = dataReader.GetString(12),
                        Country = dataReader.GetString(13)
                    },
                    Car = new CarModel
                    {
                        Id = dataReader.GetInt32(14),
                        Location = new LocationModel
                        {
                            Id = dataReader.GetInt32(15),
                            Address = dataReader.GetString(16),
                            City = dataReader.GetString(17),
                            Zipcode = dataReader.GetString(18),
                            Country = dataReader.GetString(19)
                        },
                        Price = dataReader.GetDouble(20),
                        Brand = dataReader.GetString(21),
                        Model = dataReader.GetString(22),
                        Color = dataReader.GetString(23),
                        Doors = dataReader.GetInt32(24),
                        AnimalsAllowed = dataReader.GetBoolean(25),
                        Available = dataReader.GetBoolean(26),
                        CarType = dataReader.GetString(27),
                        FuelType = dataReader.GetString(28),
                        LicensePlate = dataReader.GetString(29)
                    },
                    Customer = new CustomerModel
                    {
                        Id = dataReader.GetInt32(30),
                        FirstName = dataReader.GetString(31),
                        LastName = dataReader.GetString(32),
                        Gender = dataReader.GetString(33),
                        DriversLicense = dataReader.GetString(34)
                    }
                });
            }
            dataReader.Close();
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                this.CloseConnection();
            }
            return bookings;
        }

        public BookingId CreateBookingModel(BookingModel bookingModel)
        {
            string query = $"INSERT INTO Bookings (fk_pickup_location_id, fk_dropoff_location_id, fk_car_id, fk_customer_id, pickup_date, dropoff_date, is_cancelled)" +
                $"VALUES ('{bookingModel.PickupLocation.Id}', '{bookingModel.DropoffLocation.Id}', '{bookingModel.Car.Id}', '{bookingModel.Customer.Id}', '{bookingModel.PickupDate}', '{bookingModel.DropoffDate}', 0);";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                this.OpenConnection();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            BookingId bookingId = new BookingId();
            bookingId.Id = Convert.ToInt32(cmd.LastInsertedId);
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                this.CloseConnection();
            }
            return bookingId;
        }

        public AffectedRows CancelBookingModel(BookingModel bookingModel)
        {
            string query = $"UPDATE Bookings SET is_cancelled = 1 WHERE id = '{bookingModel.Id}';";
            if (connection.State != System.Data.ConnectionState.Open)
            {
                this.OpenConnection();
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            AffectedRows affectedRows = new AffectedRows();
            affectedRows.AffectedRows_ = Convert.ToInt32(cmd.ExecuteNonQuery());
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                this.CloseConnection();
            }
            return affectedRows;
        }
    }
}

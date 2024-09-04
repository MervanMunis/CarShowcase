// app.ts
console.log("Car listing page initialized");
// Sample data for car listing
// Sample data for car listing
var cars = [
    {
        id: 1,
        brand: "Toyota",
        model: "Camry",
        image: "/assets/1.jpeg",
        description: "A reliable sedan with great fuel efficiency.",
        features: ["Airbags", "ABS", "Sunroof"],
        year: 2020
    },
    {
        id: 2,
        brand: "Honda",
        model: "Civic",
        image: "/assets/3.jpeg",
        description: "A compact car with excellent handling.",
        features: ["Cruise Control", "Backup Camera", "Bluetooth"],
        year: 2021
    },
    {
        id: 3,
        brand: "Ford",
        model: "Mustang",
        image: "/assets/3.jpeg",
        description: "A powerful muscle car with stunning performance.",
        features: ["V8 Engine", "Sport Mode", "Leather Seats"],
        year: 2019
    },
    {
        id: 4,
        brand: "Ford",
        model: "Mustang",
        image: "/assets/3.jpeg",
        description: "A powerful muscle car with stunning performance.",
        features: ["V8 Engine", "Sport Mode", "Leather Seats"],
        year: 2019
    },
    {
        id: 5,
        brand: "Ford",
        model: "Mustang",
        image: "/assets/3.jpeg",
        description: "A powerful muscle car with stunning performance.",
        features: ["V8 Engine", "Sport Mode", "Leather Seats"],
        year: 2019
    },
    {
        id: 6,
        brand: "Ford",
        model: "Mustang",
        image: "/assets/3.jpeg",
        description: "A powerful muscle car with stunning performance.",
        features: ["V8 Engine", "Sport Mode", "Leather Seats"],
        year: 2019
    },
    // Add more cars here...
];
// Function to display cars
function displayCars(carList) {
    var carListDiv = document.getElementById('carList');
    carListDiv.innerHTML = '';
    carList.forEach(function (car) {
        var carCard = document.createElement('div');
        carCard.className = 'bg-white shadow rounded overflow-hidden flex flex-col items-center cursor-pointer transform transition duration-300 hover:scale-105';
        // Set fixed width and height for the image container and apply hover animation
        carCard.innerHTML = "\n          <div class=\"w-64 h-48 bg-gray-200 overflow-hidden flex items-center justify-center\">\n              <img src=\"".concat(car.image, "\" alt=\"").concat(car.brand, " ").concat(car.model, "\" class=\"w-full h-full object-cover\">\n          </div>\n          <div class=\"p-4 w-full text-center\">\n              <h2 class=\"font-bold text-lg\">").concat(car.brand, " ").concat(car.model, "</h2>\n          </div>\n      ");
        // Add click event to redirect to car details page
        carCard.addEventListener('click', function () {
            // Redirect to car-details.html with the car ID in the URL
            window.location.href = "car-details.html?id=".concat(car.id);
        });
        carListDiv.appendChild(carCard);
    });
}
// Initialize car list
displayCars(cars);

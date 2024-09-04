// Sample data for car listing (same data from app.ts)
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
        image: "/assets/1.jpeg",
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
    // Add more cars...
];
// Function to get the car ID from the URL
function getCarIdFromUrl() {
    var urlParams = new URLSearchParams(window.location.search);
    var carId = urlParams.get('id');
    return carId ? parseInt(carId) : null;
}
// Function to display the car details
function displayCarDetails(car) {
    var carTitle = document.getElementById('carTitle');
    var carImageContainer = document.getElementById('carImageContainer');
    var carDetailsContainer = document.getElementById('carDetailsContainer');
    // Set title
    carTitle.textContent = "".concat(car.brand, " ").concat(car.model, " Details");
    // Display the car image
    carImageContainer.innerHTML = "\n        <img src=\"".concat(car.image, "\" alt=\"").concat(car.brand, " ").concat(car.model, "\" class=\"w-full h-auto rounded-lg\">\n    ");
    // Display car details
    carDetailsContainer.innerHTML = "\n        <h2 class=\"text-2xl font-bold mb-4\">".concat(car.brand, " ").concat(car.model, " (").concat(car.year, ")</h2>\n        <p class=\"mb-4 text-gray-700\"><strong>Description:</strong> ").concat(car.description, "</p>\n        <h3 class=\"text-xl font-bold mb-2\">Features:</h3>\n        <ul class=\"list-disc ml-6 text-gray-700\">\n            ").concat(car.features.map(function (feature) { return "<li>".concat(feature, "</li>"); }).join(''), "\n        </ul>\n    ");
}
// Main function to load car details
function loadCarDetails() {
    var carId = getCarIdFromUrl();
    if (carId !== null) {
        var car = cars.find(function (c) { return c.id === carId; });
        if (car) {
            displayCarDetails(car);
        }
        else {
            alert('Car not found');
        }
    }
    else {
        alert('No car ID specified');
    }
}
// Load the car details when the page is loaded
window.onload = loadCarDetails;

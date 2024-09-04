// Sample data for car listing (same data from app.ts)
const cars = [
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
function getCarIdFromUrl(): number | null {
    const urlParams = new URLSearchParams(window.location.search);
    const carId = urlParams.get('id');
    return carId ? parseInt(carId) : null;
}

// Function to display the car details
function displayCarDetails(car: any) {
    const carTitle = document.getElementById('carTitle')!;
    const carImageContainer = document.getElementById('carImageContainer')!;
    const carDetailsContainer = document.getElementById('carDetailsContainer')!;

    // Set title
    carTitle.textContent = `${car.brand} ${car.model} Details`;

    // Display the car image
    carImageContainer.innerHTML = `
        <img src="${car.image}" alt="${car.brand} ${car.model}" class="w-full h-auto rounded-lg">
    `;

    // Display car details
    carDetailsContainer.innerHTML = `
        <h2 class="text-2xl font-bold mb-4">${car.brand} ${car.model} (${car.year})</h2>
        <p class="mb-4 text-gray-700"><strong>Description:</strong> ${car.description}</p>
        <h3 class="text-xl font-bold mb-2">Features:</h3>
        <ul class="list-disc ml-6 text-gray-700">
            ${car.features.map((feature: string) => `<li>${feature}</li>`).join('')}
        </ul>
    `;
}

// Main function to load car details
function loadCarDetails() {
    const carId = getCarIdFromUrl();

    if (carId !== null) {
        const car = cars.find(c => c.id === carId);
        if (car) {
            displayCarDetails(car);
        } else {
            alert('Car not found');
        }
    } else {
        alert('No car ID specified');
    }
}

// Load the car details when the page is loaded
window.onload = loadCarDetails;

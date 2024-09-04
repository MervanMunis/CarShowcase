
document.getElementById('carForm')!.addEventListener('submit', function (event) {
    event.preventDefault();
    const brand = (document.getElementById('brand') as HTMLInputElement).value;
    const model = (document.getElementById('model') as HTMLInputElement).value;
    const imageFile = (document.getElementById('image') as HTMLInputElement).files![0];

    // Logic to upload the car data to your backend API
    console.log({ brand, model, imageFile });
});

document.getElementById('uploadJson')!.addEventListener('click', function () {
    const jsonFile = (document.getElementById('jsonFile') as HTMLInputElement).files![0];

    if (jsonFile) {
        const reader = new FileReader();
        reader.onload = function (e) {
            const jsonData = JSON.parse(e.target!.result as string);
            console.log(jsonData);

            // Logic to send JSON data to your backend API
        };
        reader.readAsText(jsonFile);
    }
});

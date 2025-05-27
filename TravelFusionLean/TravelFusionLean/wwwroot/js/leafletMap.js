export function initializeMapWithData(data) {
    const map = L.map('map').setView([data.latitude, data.longitude], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: 'Map data © OpenStreetMap contributors'
    }).addTo(map);

    L.marker([data.latitude, data.longitude])
        .addTo(map)
        .bindPopup(`<b>${data.hotelName}</b>`)
        .openPopup();
}
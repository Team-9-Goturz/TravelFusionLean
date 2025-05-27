export function initializeMap() {
    const map = L.map('map').setView([56.4607, 10.0364], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '© OpenStreetMap-bidragsydere'
    }).addTo(map);

    L.marker([56.4607, 10.0364])
        .addTo(map)
        .bindPopup("Hotel Randers")
        .openPopup();
}
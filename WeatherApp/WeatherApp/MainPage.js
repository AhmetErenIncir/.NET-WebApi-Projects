function Getweather() {
    function Getweather() {
        fetch('http://localhost:5048/weather', {
            method: 'GET'
        })
        .then(response => response.text())
        .then(data => {
            console.log(data);  // Yanıt verisi burada erişilebilir olmalı
            document.getElementById("Getweather").innerHTML = `Hava ${data} derece`;
        })
        .catch(error => console.error("Error:", error));
    }
    Getweather();
    
}



async function fetchData() {
    const response = await fetch('http://localhost:5048'); // URL'yi kendi API'nize göre güncelleyin
    const data = await response.json(); // JSON formatında bir yanıt aldığımızı varsayıyoruz
    return data; // Dönen veri burada `data` objesinde olacak
}

// Çağırırken


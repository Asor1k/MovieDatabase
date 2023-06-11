const uri = 'api/movies';
let movies = [];

function getMovies() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayMovies(data))
        .catch(error => console.error('Unable to get items.', error));
}

function getMoviesByTitle(title) {
    fetch(uri + `/${title}`, {
        method: 'GET'
    })
        .then(response => response.json())
        .then(data => _displayMovies(data))
        .catch(error => console.error('Unable to get items.', error));
}

function getMovieElement(title, image, description, wikipediaUrl) {
    var movie = "\<tr class\=\"movieCell\"\>";
    movie += `\<td class\=\"movieImage\"\>\<img src\=\"${image}\"\<\/td\>\<br\>`;
    movie += `\<td class\=\"movieTitle\"\>${title}\<\/td\>\<br\>`;
    movie += `\<td class\=\"movieDescription\"\>${description}\<\/td\><br>`;
    movie += `\<td class\=\"movieWikipediaUrl\" onclick=\'window.open(${wikipediaUrl})\'\>${wikipediaUrl}\<\/td\>`;
    movie += "\<\/tr\>";
    return movie;
}

function _displayMovies(data) {
    const table = document.getElementById("movieTable");
    table.innerHTML = "";
    let i = 0;
    let numberOfElementsInRow = 3;
    let currentRow = table.insertRow();
    data.forEach(movie => {
        const element = (getMovieElement(movie["name"], movie["imagePreview"], movie["description"], movie["wikipediaURL"]));

        if (i % numberOfElementsInRow == 0) {
            i = 0;
            currentRow = table.insertRow();
        }
        i += 1;

        let cell = currentRow.insertCell();
        cell.innerHTML = element; 
    });
}

const inputField = document.getElementById('titleInput');
inputField.addEventListener("change", (event) => {
    getMoviesByTitle(event.target.value);
});

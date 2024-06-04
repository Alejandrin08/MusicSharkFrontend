const imagen = document.querySelector(".image");
const archivo = document.getElementById("Image");

function CargaImagen() {
    const file = archivo.files;
    if (file.length) {
        const fileReader = new FileReader();
        fileReader.onload = function (event) {
            imagen.setAttribute('src', event.target.result);
        }
        fileReader.readAsDataURL(file[0]);
    }
}

archivo.addEventListener("change", CargaImagen);
CargaImagen();
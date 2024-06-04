const imagen = document.querySelector(".image");
const archivo = document.getElementById("FileId");

function CargaImagen() {

    if (archivo.selectedIndex > 0) {
        const path = imagen.dataset.url + "/api/file/" + archivo.options[archivo.selectedIndex].value;
        imagen.src = path;
    }
}

archivo.addEventListener("change", CargaImagen);
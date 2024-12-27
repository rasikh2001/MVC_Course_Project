document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("send-message").addEventListener("click", function () {
        const message = document.getElementById("message-input").value;
        if (message.trim()) {
            const messageDiv = document.createElement("div");
            messageDiv.classList.add("message");
            messageDiv.textContent = message;
            document.getElementById("chat-box").appendChild(messageDiv);
            document.getElementById("message-input").value = "";
        }
    });

    document.getElementById("upload-image").addEventListener("click", function () {
        new bootstrap.Modal(document.getElementById('uploadModal')).show();
    });

    document.getElementById("upload-image-btn").addEventListener("click", function () {
        const file = document.getElementById("image-upload").files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                const imageDiv = document.createElement("div");
                imageDiv.classList.add("image-message");
                const image = document.createElement("img");
                image.src = e.target.result;
                image.classList.add("img-fluid");
                imageDiv.appendChild(image);
                document.getElementById("image-display").appendChild(imageDiv);
                new bootstrap.Modal(document.getElementById('uploadModal')).hide();
            };
            reader.readAsDataURL(file);
        }
    });
});

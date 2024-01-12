<!-- Creative Collab Section -->
<section id="creativeCollab">
    <!--
    The CreativeCollab section allows users to upload artwork and add comments.
    -->

    <!-- Heading for the CreativeCollab section -->
    <h2>CreativeCollab - What masterpiece will you share today?</h2>

    <!-- Artwork Upload Form -->
    <form id="artworkForm">
        <!-- Label for the artwork upload input -->
        <label for="artwork">Upload Your Artwork:</label>
        <!-- Input field for uploading artwork -->
        <input type="file" id="artwork" name="artwork" accept="image/*" required>

        <!-- Label for the comment input -->
        <label for="comment">Add a Comment:</label>
        <!-- Textarea for entering comments -->
        <textarea id="comment" name="comment" rows="4" required></textarea>

        <!-- Button to submit the artwork form -->
        <button type="submit">Post Artwork</button>
    </form>

    <!-- Artwork Display Area -->
    <div id="artworkDisplay">
        <!-- Artwork and comments will be dynamically added here -->
    </div>
</section>

<!-- JavaScript (Sample, requires additional functionality for image handling) -->
<script>
    /*
    JavaScript code for handling artwork submissions and display.
    */

    document.getElementById("artworkForm").addEventListener("submit", function(event) {
        event.preventDefault();

        var artworkInput = document.getElementById("artwork");
        var commentInput = document.getElementById("comment");

        var artworkDisplay = document.getElementById("artworkDisplay");

        // Create a new artwork container
        var artworkContainer = document.createElement("div");
        artworkContainer.classList.add("artwork");

        // Display the uploaded image (requires server-side handling for security)
        var img = document.createElement("img");
        img.src = URL.createObjectURL(artworkInput.files[0]);
        artworkContainer.appendChild(img);

        // Display the comment
        var commentsDiv = document.createElement("div");
        commentsDiv.classList.add("comments");
        var commentParagraph = document.createElement("p");
        commentParagraph.textContent = commentInput.value;
        commentsDiv.appendChild(commentParagraph);

        artworkContainer.appendChild(commentsDiv);

        // Append the artwork container to the display area
        artworkDisplay.appendChild(artworkContainer);

        // Clear form inputs
        artworkInput.value = "";
        commentInput.value = "";
    });
</script>

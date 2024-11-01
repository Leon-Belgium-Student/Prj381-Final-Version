pip install flask
pip install tensorflow

from flask import Flask, request, jsonify
from PIL import Image
import io
import numpy as np
import tensorflow as tf  # Assuming you're using TensorFlow; replace as needed

app = Flask(Grp3ImageProcess)

# Load your pre-trained model (adjust as per your framework, e.g., TensorFlow, PyTorch)
model = tf.keras.models.load_model('C:\Users\leonb\Documents\STS projects\PRJ381-2-\Scripts.download_and_organize_images.py')  # Example for TensorFlow

def prepare_image(image):
    # Preprocess the image according to your model's requirements
    image = image.resize((224, 224))  # Example for resizing
    image = np.array(image)
    image = image / 255.0  # Normalizing (example)
    image = np.expand_dims(image, axis=0)  # Add batch dimension
    return image


@app.route('/upload', methods=['POST'])
def upload_image():
    if 'file' not in request.files:
        return jsonify({'error': 'No file part'})

    file = request.files['file']

    if file.filename == '':
        return jsonify({'error': 'No selected file'})

    if file:
        # Open the image file
        image = Image.open(io.BytesIO(file.read()))
        processed_image = prepare_image(image)

        # Make prediction
        predictions = model.predict(processed_image)
        # Add post-processing based on your model's output
        predicted_class = np.argmax(predictions, axis=1)

        # Return the result as a JSON response
        return jsonify({'prediction': int(predicted_class[0])})

@app.route('/status', methods=['GET'])
def status():
    return jsonify({'status': 'API is running!'})

if __name__ == '__main__':
    app.run(debug=True)

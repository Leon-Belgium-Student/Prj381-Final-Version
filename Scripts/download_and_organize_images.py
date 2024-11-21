import pandas as pd
import os
import requests
from tqdm import tqdm

file_path = 'C:\\Users\\user-pc\\Documents\\PRJ381(2)\\data\\Aloe ferox PHENOLOGY.xlsx'
base_dir = 'Images'
classes = ['flower', 'bud', 'fruit', 'no_evidence']

#Directories for train, validation and test sets
for split in ['train', 'validation', 'test']:
    for cls in classes:
        os.makedirs(os.path.join(base_dir, split, cls), exist_ok=True)

#Image downloading function
def download_images(sheet_name, class_name):
    df = pd.read_excel(file_path, sheet_name=sheet_name)

    if 'image_url' not in df.columns:
        print(f"Sheet '{sheet_name}' does not contain an 'image_url' column.")
        return
    #Loop for going throught every row
    for index, row in tqdm(df.iterrows(), total=df.shape[0], desc=f"Downloading {class_name} images"):
        image_url = row['image_url']
        image_name = f"{class_name}_{index}.jpg"
        split = 'train'

        #Path where images will be saved
        image_path = os.path.join(base_dir, split, class_name, image_name)

        try:
            #HTTP GET request to fetch the image
            response = requests.get(image_url, stream=True, timeout=10)
            if response.status_code == 200:
                with open(image_path, 'wb') as file:
                    for chunk in response.iter_content(1024):
                        file.write(chunk)
            else:
                print(f"Failed to download {image_url}: Status code {response.status_code}")
        except Exception as e:
            print(f"Failed to download {image_url}: {e}")

download_images('FLOWERS', 'flower')
download_images('BUDS', 'bud')
download_images('FRUIT', 'fruit')
download_images('No Evidence', 'no_evidence')
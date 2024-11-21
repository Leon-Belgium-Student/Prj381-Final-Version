import os
import shutil
import random

#Dataset paths
base_dir = 'Images'
train_dir = os.path.join(base_dir, 'train')
validation_dir = os.path.join(base_dir, 'validation')
test_dir = os.path.join(base_dir, 'test')

os.makedirs(validation_dir, exist_ok=True)
os.makedirs(test_dir, exist_ok=True)

#Validation and test splits (15% of images each)
val_split = 0.15
test_split = 0.15

for class_name in os.listdir(train_dir):
    class_path = os.path.join(train_dir, class_name)

    if os.path.isdir(class_path):
        os.makedirs(os.path.join(validation_dir, class_name), exist_ok=True)
        os.makedirs(os.path.join(test_dir, class_name), exist_ok=True)
        
        #Gets all the images and shuffles them
        images = os.listdir(class_path)
        random.shuffle(images)
        
        #Calculation for number of images for validation and test splits
        total_images = len(images)
        val_size = int(total_images * val_split)
        test_size = int(total_images * test_split)
        
        #Actual splitting of the images
        val_images = images[:val_size]
        test_images = images[val_size:val_size + test_size]
        
        #Move images to respective directories
        for image in val_images:
            src_path = os.path.join(class_path, image)
            dst_path = os.path.join(validation_dir, class_name, image)
            shutil.move(src_path, dst_path)

        for image in test_images:
            src_path = os.path.join(class_path, image)
            dst_path = os.path.join(test_dir, class_name, image)
            shutil.move(src_path, dst_path)

print("Images have been split into train, validation, and test sets.")
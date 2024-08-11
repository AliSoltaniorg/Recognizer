# Use the official Python base image (choose the desired version)
FROM python:3.9
ENV PYTHONUNBUFFERED 1
# Update the package lists
RUN apt-get update && apt-get install -y cmake
RUN apt-get install -y libgl1-mesa-dev
# Set the working directory within the container
WORKDIR /app

# shorter and safer to COPY into current directory
COPY src/PyFace/requirements.txt ./

# Install dependencies (if you have a requirements.txt file)
RUN pip install --upgrade pip
RUN pip install -r requirements.txt

# Copy your application code into the container
COPY src/PyFace .

# Specify the command to run your Python script (e.g., your main application file)
CMD ["python", "/app/PyFace.py"]

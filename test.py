import requests

url = "https://api.open-meteo.com/v1/forecast?latitude=21.03&longitude=105.85&current_weather=true"

response = requests.get(
    url
)  # tong hop du lieu lai thanh mot mang hay dictionary, dai loai v
data = response.json()

print(response)
print(data["current_weather"]["time"])
print(
    f"{data['current_weather']['temperature']}{data['current_weather_units']['temperature']} "
)

extends AudioStreamPlayer

var recording = true
var busIndex
func _ready():
	busIndex = AudioServer.get_bus_index("Recording")
	var record:AudioEffectRecord = AudioServer.get_bus_effect(busIndex,0)
	record.set_recording_active(true)
	var stmi:AudioStreamSample = record.get_recording()
	#set_stream(stmi)
	print(AudioServer.get_bus_peak_volume_left_db(busIndex,0))
	yield(get_tree().create_timer(3),"timeout")
	print(AudioServer.get_bus_peak_volume_left_db(busIndex,0))
	recording=false
	var audio_stream:AudioStreamSample = record.get_recording()
	audio_stream.save_to_wav('res://tessdasdast.wav')
	
func _process(delta):
	if recording:
		print(get_stream());
		print(AudioServer.get_bus_peak_volume_left_db(busIndex,0))
		print(AudioServer.get_bus_peak_volume_right_db(busIndex,0))
		print(AudioServer.get_bus_peak_volume_left_db(0,0))
		
	

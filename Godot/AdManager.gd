extends Node

var _ad_view : AdView
var _ad_listener := AdListener.new()
var _is_initialized := false

func _ready() -> void:
	if _is_initialized or (OS.get_name() != "Android" and OS.get_name() != "iOS"):
		return
	call_deferred("_initialize")

func _initialize() -> void:
	_ad_listener.on_ad_loaded = _on_ad_loaded
	var on_init := OnInitializationCompleteListener.new()
	on_init.on_initialization_complete = _on_admob_initialized
	MobileAds.initialize(on_init)
	_is_initialized = true

func _on_admob_initialized(_status: InitializationStatus) -> void:
	if OS.get_name() == "Android":
		_create_ad_view()

func _create_ad_view() -> void:
	if _ad_view:
		_ad_view.destroy()
	_ad_view = null
	var unit_id : String = "ADMOB BANNER-ID"
	_ad_view = AdView.new(unit_id, AdSize.BANNER, AdPosition.BOTTOM)
	_ad_view.ad_listener = _ad_listener
	_ad_view.load_ad(AdRequest.new())


func _on_ad_loaded() -> void:
	if _ad_view:
		_ad_view.show()

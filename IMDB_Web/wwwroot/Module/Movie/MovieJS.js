var objMovieList = [];
const EmptyGuid = "00000000-0000-0000-0000-000000000000";
$(document).ready(function () {
	MoviePageLoad();

	$(document).on('click', '.toggle-sidebar-btn', function () {
		if ($('body').hasClass('toggle-sidebar')) {
			$('body').removeClass('toggle-sidebar');
		}
		else {
			$('body').addClass('toggle-sidebar');
        }
		
	});

	$(document).on('click', '#btnAddMovie', function () {
		$('#divMovie').show();
		$('#divProducers').hide();
		$('#divActors').hide();
		$("#rdoMovie").prop("checked", true);
		$('#myModelLabel').html('Add New Movie');
		$('#movieModal').modal('show');
	});

	$(document).on('click', '#rdoMovie', function () {
		$('#divMovie').show();
		$('#divProducers').hide();
		$('#divActors').hide();
		ClearFields();
	});

	$(document).on('click', '#rdoProducer', function () {
		$('#divMovie').hide();
		$('#divProducers').show();
		$('#divActors').hide();
		ClearFields();
	});

	$(document).on('click', '#rdoActor', function () {
		$('#divMovie').hide();
		$('#divProducers').hide();
		$('#divActors').show();
		ClearFields();
	});

	$(document).on('click', '#btnMovieClose', function () {
		ClearFields();
	});

	$(document).on('click', '#btnMovieCancel', function () {
		ClearFields();
	});

	$(document).on('click', '#btnSave', function () {

		if ($("#rdoMovie").is(":checked")) {
			if ($('#formMovie').valid()) {
				if ($('hdnMovieId').val() != EmptyGuid) {
					UpdateMovie();
				}
				else {
					var objMovieLists = objMovieList.filter(m => m.MovieName == $('#txtMovieName').val() && m.IsActive == true);
					if ($.isEmptyObject(objMovieLists)) {
						SaveMovie();
					}
					else {
						toastr.info('This movie already in your list, please select other one.');
					}
                }
				
			}
		}

		if ($("#rdoProducer").is(":checked")) {
			if ($('#formProducer').valid()) {
				SaveProducer();
			}
		}

		if ($("#rdoActor").is(":checked")) {
			if ($('#formActor').valid()) {
				SaveActor();
			}
		}
	});

	$(document).on('click', '.btnMovieEdit', function () {
		var movieId = $(this).attr('data-movieid');
		var objMovieLists = objMovieList.filter(m => m.MovieId == movieId && m.IsActive == true);
		if (!$.isEmptyObject(objMovieLists)) {
			$('#divMovie').show();
			$('#divProducers').hide();
			$('#divActors').hide();
			$("#rdoMovie").prop("checked", true);
			$('#myModelLabel').html('Edit Movie');
			$('#movieModal').modal('show');

			$('#hdnMovieId').val(objMovieLists[0].MovieId);
			$('#ddlProducer').val(objMovieLists[0].ProducerId);
			$('#txtMovieName').val(objMovieLists[0].MovieName);
			$('#txtMovieReleaseYear').val(objMovieLists[0].MovieReleaseYear);
			$('#txtMoviePlot').val(objMovieLists[0].MoviePlot);
			$('#txtMoviePoster').val(objMovieLists[0].MoviePoster);
		}
    })

	$(document).on('click', '.btnMovieDelete', function () {
		if (confirm('Are you sure you want to delete this movie ?')) {
			var movieId = $(this).attr('data-movieid');
			var form_data = new FormData();
			form_data.append("MOVIEID", movieId);

			$.ajax({
				type: "GET",
				url: "/Movie/DeleteMovie",
				data: { movieId: movieId },
				contentType: 'application/x-www-form-urlencoded; charset=utf-8',
				async: false,
				dataType: "json",
				success: function (data) {
					if (data && data != "undefined") {
						if (data.Message == 'Record deleted successfully.') {
							toastr.success(data.Message);
							GetAllMovieList();
						}
						else {
							toastr.error(data.Message);
                        }
					}
				},

			});
		}
		else {
			return false;
		}
	});

});

function MoviePageLoad() {
	GetAllMovieList();
	GetAllProducers();
	GetAllActors();
	$('#ddlActor').select2({
		placeholder: "Please select actors",
		allowClear: true,
		dropdownParent: $("#movieModal"),
		width: '100%'
	});
}

function ClearFields() {
	$('#hdnMovieId').val("");
	$('#ddlProducer').val("");
	$('#txtMovieName').val("");
	$('#txtMovieReleaseYear').val("");
	$('#txtMoviePlot').val("");
	$('#txtMoviePoster').val("");
	$('#ddlActor').val(null).trigger('change');

	$('#txtProducerName').val("");
	$('#rdoMale').prop("checked", true);
	$('#txtProducerDateOfBirth').val("");
	$('#txtProducerBio').val("");

	$('#txtActorName').val("");
	$('#rdoActorMale').prop("checked", true);
	$('#txtActorDateOfBirth').val("");
	$('#txtActorBio').val("");
	$.each($('#formActor .field-validation-error'), function () {
		$(this).html('');
	});

	$.each($('#formProducer .field-validation-error'), function () {
		$(this).html('');
	});

	$.each($('#formMovie .field-validation-error'), function () {
		$(this).html('');
	});
}

function GetAllMovieList() {
	$.ajax({
		type: "GET",
		url: "/Movie/GetAllMovieList",
		data: '{}',
		contentType: "application/json",
		dataType: "json",
		success: function (data) {
			if (data.Message == "Movie data getting successfully.") {
				toastr.success(data.Message);
				objMovieList = data.Data;
				BindMovieList();
			}
		},
		failure: function (response) {
			toastr.error("Error Occured");
		},
		error: function (response) {
			toastr.error("Error Occured");
		}
	});
}

function BindMovieList() {
	if (!$.isEmptyObject(objMovieList)) {
		var tbodyMovie = $('#tbodyMovie').empty();
		$.each(objMovieList, function (index, movie) {
			var movieRow = `<tr>
                                <th scope="row">{{ROWNO}}</th>
                                <td>{{MOVIENAME}}</td>
                                <td>{{YEAR}}</td>
                                <td>{{PRODUCER}}</td>
                                <td>{{ACTORS}}</td>
                                <td>{{ACTION}}</td>
                            </tr>`;

			var btnMovieEdit = '<a href="javascript:void(0)" class="btnMovieEdit" data-movieid="{MOVIEID}"><i class="ri-edit-2-line"></i></a>';
			btnMovieEdit = btnMovieEdit.replace('{MOVIEID}', movie.MovieId);

			var btnMovieDelete = '<a href="javascript:void(0)" class="btnMovieDelete" data-movieid="{MOVIEID}"><i class="ri-delete-bin-2-line"></i></a>';
			btnMovieDelete = btnMovieDelete.replace('{MOVIEID}', movie.MovieId);

			movieRow = movieRow.replace('{{ROWNO}}', index + 1);
			movieRow = movieRow.replace('{{MOVIENAME}}', movie.MovieName);
			movieRow = movieRow.replace('{{YEAR}}', movie.MovieReleaseYear);
			movieRow = movieRow.replace('{{PRODUCER}}', movie.ProducerName);
			movieRow = movieRow.replace('{{ACTORS}}', movie.Actors);
			movieRow = movieRow.replace('{{ACTION}}', btnMovieEdit + ' ' + btnMovieDelete);
			tbodyMovie.append(movieRow);
		});
    }
}

function GetAllProducers() {
	var ddlProducer = $('#ddlProducer');
	$.ajax({
		type: "GET",
		url: "/Movie/GetAllProducersList",
		data: '{}',
		contentType: "application/json",
		dataType: "json",
		success: function (data) {
			if (data) {
				ddlProducer.empty().append('<option selected="selected" value="">Please select producer</option>');
				$.each(data.Data, function (index, value) {
					ddlProducer.append($("<option></option>").val(value.ProducerId).html(value.Name));
				});
			}
		},
		failure: function (response) {
			toastr.error("Error Occured");
			//alert(response.responseText);
		},
		error: function (response) {
			toastr.error("Error Occured");
			//alert(response.responseText);
		}
	});
}

function GetAllActors() {
	var ddlActor = $('#ddlActor');
	$.ajax({
		type: "GET",
		url: "/Movie/GetAllActorsList",
		data: '{}',
		contentType: "application/json",
		dataType: "json",
		success: function (data) {
			if (data) {
				//ddlActor.empty().append('<option selected="selected" value="">Please select actors</option>');
				$.each(data.Data, function (index, value) {
					ddlActor.append($("<option></option>").val(value.ActorId).html(value.ActorName));
				});
			}
		},
		failure: function (response) {
			toastr.error("Error Occured");
		},
		error: function (response) {
			toastr.error("Error Occured");
		}
	});
}

function SaveMovie() {
	var MovieRequest = {
		MovieId: EmptyGuid,
		ProducerId: $('#ddlProducer').val(),
		MovieName: $('#txtMovieName').val(),
		MovieReleaseYear: $('#txtMovieReleaseYear').val(),
		MoviePlot: $('#txtMoviePlot').val(),
		MoviePoster: '',
		ActorsId: $('#ddlActor').val()
	};

	var form_data = new FormData();
	form_data.append("MOVIEREQUEST", JSON.stringify(MovieRequest));
	form_data.append("MOVIEIMGREQUEST", document.getElementById('txtMoviePoster').files[0]);

	$.ajax({
		type: "POST",
		url: "/Movie/InsertMovie",
		data: form_data,
		contentType: false,
		processData: false,
		dataType: "json",
		async: false,
		success: function (data) {

			if (data && data != "undefined") {
				if (data.Message == "Record added successfully.") {
					toastr.success(data.Message);
					$('#movieModal').modal('hide');
					GetAllMovieList();
					ClearFields();
				}
				else {
					toastr.error(data.Message);
				}
			}
		},
		failure: function (response) {
			toastr.error("Error Occured");
		},
		error: function (response) {
			toastr.error("Error Occured");
		}
	});
}

function UpdateMovie() {
	var MovieRequest = {
		MovieId: $('#hdnMovieId').val(),
		ProducerId: $('#ddlProducer').val(),
		MovieName: $('#txtMovieName').val(),
		MovieReleaseYear: $('#txtMovieReleaseYear').val(),
		MoviePlot: $('#txtMoviePlot').val(),
		MoviePoster: '',
		ActorsId: $('#ddlActor').val()
	};

	var form_data = new FormData();
	form_data.append("MOVIEREQUEST", JSON.stringify(MovieRequest));
	form_data.append("MOVIEIMGREQUEST", document.getElementById('txtMoviePoster').files[0]);


	$.ajax({
		type: "POST",
		url: "/Movie/UpdateMovie",
		data: form_data,
		contentType: false,
		processData: false,
		dataType: "json",
		async: false,
		success: function (data) {

			if (data && data != "undefined") {
				if (data.Message == "Record updated successfully.") {
					toastr.success(data.Message);
					$('#movieModal').modal('hide');
					GetAllMovieList();
					ClearFields();
				}
				else {
					toastr.error(data.Message);
				}
			}
		},
		failure: function (response) {
			toastr.error("Error Occured");
		},
		error: function (response) {
			toastr.error("Error Occured");
		}
	});
}

function SaveActor() {
	var gender = "Female";
	if ($('#rdoActorMale').is(':checked')) {
		gender = "Male"
	}
	var ActorRequest = {
		ActorId: EmptyGuid,
		ActorName: $('#txtActorName').val(),
		ActorSex: gender,
		ActorDob: $('#txtActorDateOfBirth').val(),
		ActorBio: $('#txtActorBio').val()
	};

	var form_data = new FormData();
	form_data.append("ACTORREQUEST", JSON.stringify(ActorRequest));

	$.ajax({
		type: "POST",
		url: "/Movie/InsertActor",
		data: form_data,
		contentType: false,
		processData: false,
		dataType: "json",
		async: false,
		success: function (data) {

			if (data && data != "undefined") {
				if (data.Message == "Record added successfully.") {
					toastr.success(data.Message);
					ClearFields();
				}
				else {
					toastr.error(data.Message);
				}
			}
		},
		failure: function (response) {
			toastr.error("Error Occured");
		},
		error: function (response) {
			toastr.error("Error Occured");
		}
	});
}

function SaveProducer() {
	var gender = "Female";
	if ($('#rdoMale').is(':checked')) {
		gender = "Male"
    }
	var ProducerRequest = {
		ProducerId: EmptyGuid,
		Name: $('#txtProducerName').val(),
		Sex: gender,
		Dob: $('#txtProducerDateOfBirth').val(),
		Bio: $('#txtProducerBio').val()
	};

	var form_data = new FormData();
	form_data.append("PRODUCERREQUEST", JSON.stringify(ProducerRequest));

	$.ajax({
		type: "POST",
		url: "/Movie/InsertProducer",
		data: form_data,
		contentType: false,
		processData: false,
		dataType: "json",
		async: false,
		success: function (data) {

			if (data && data != "undefined") {
				if (data.Message == "Record added successfully.") {
					toastr.success(data.Message);
					ClearFields();
				}
				else {
					toastr.error(data.Message);
				}
			}
		},
		failure: function (response) {
			toastr.error("Error Occured");
		},
		error: function (response) {
			toastr.error("Error Occured");
		}
	});
}
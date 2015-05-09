Array.prototype.count = function() {
  return this.length;
};

Array.prototype.sum = function(){
  var result = 0;
  for (var count = 0; count < this.length; count++) {
	  if(typeof(this[count]) === 'number'){
		result = result + this[count];
	  }
  };
  return result;
}

Array.prototype.take = function(position) {
	if(position > this.count()){
		return [];
	}
	return this.slice(0, position);
};

Array.prototype.where = function(condition){
	var result = [];
	for(var count = 0; count < this.count(); count++){
		if(condition(this[count])){
			result.push(this[count]);
		}
	}
	return result;	
}

Array.prototype.union = function(array){
	for(var count = 0; count < array.count(); count++){
		this.push(array[count]);
	}
	return this;	
}

Array.prototype.select = function(funct) {
	for (var count = 0; count < this.count(); count ++) {
		this[count] = funct(this[count]);
	};
	return this;
};

Array.prototype.remove = function(condition) {
	var result = [];
	for (var count = 0; count < this.count(); count ++) {
		if(condition(this[count])){
			result.push(this[count]);
		}
	};
	return result;
};

Array.prototype.clear = function(condition) {
	return [];
}

exports.remove = function(){};

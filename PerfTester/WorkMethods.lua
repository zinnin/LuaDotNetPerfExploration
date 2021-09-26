LuaIntWorkMethods = {
	Add = function(self, numberOne, numberTwo)
		return numberOne + numberTwo
	end,
	Subtract = function(self, numberOne, numberTwo)
		return numberOne - numberTwo
	end,
	Multiply = function(self, numberOne, numberTwo)
		return numberOne * numberTwo
	end,
	Divide = function(self, numberOne, numberTwo)
		return numberOne / numberTwo
	end,
	Remainder = function(self, numberOne, numberTwo)
		return numberOne % numberTwo
	end,
	Exponent = function(self, numberOne, numberTwo)
		return numberOne ^ numberTwo
	end,
	IsEqual = function(self, numberOne, numberTwo)
		return numberOne == numberTwo
	end,
	IsEqual = function(self, numberOne, numberTwo)
		return numberOne == numberTwo
	end,
	NotEqual = function(self, numberOne, numberTwo)
		return numberOne ~= numberTwo
	end,
	Greater = function(self, numberOne, numberTwo)
		return numberOne > numberTwo
	end,
	GreaterOrEqual = function(self, numberOne, numberTwo)
		return numberOne >= numberTwo
	end,
	Lesser = function(self, numberOne, numberTwo)
		return numberOne < numberTwo
	end,
	LesserOrEqual = function(self, numberOne, numberTwo)
		return numberOne <= numberTwo
	end,
}

LuaStringWorkMethods = {
	StringFlip = function(self, stringToFlip)
		if (stringToFlip == "StringFlip") then
			return "ThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongStringThisIsAVeryLongString"
		else
			return "StringFlip"
		end
	end,

	CombineStrings = function(self, stringOne, stringTwo)
		return stringOne .. stringTwo
	end,

	CombineStringsFormat = function(self, stringOne, stringTwo)
		return string.format("%s%s", stringOne, stringTwo)
	end,

	CombineManyStrings = function(self, stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen)
		return stringOne .. stringTwo .. stringThree .. stringFour .. stringFive .. stringSix .. stringSeven .. stringEight .. stringNine .. stringTen
	end,

	CombineManyStringsFormat = function(self, stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen)
		return string.format("%s%s%s%s%s%s%s%s%s%s", stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen)
	end,
}

LuaCollectionWorkMethods = {
	DictionaryOfInts = {},
	ListOfStrings = {},

	AddOrUpdateValueInDictionary = function(self, key, value)
		self.DictionaryOfInts[key] = value
	end,

	GetValueFromDictionary = function(self, key)
		if (self.DictionaryOfInts[key]) then
			return self.DictionaryOfInts[key]
		end

		return 0
	end,

	AddStringToList = function(self, newString)	
		table.insert(self.ListOfStrings, newString)
	end,

	-- There is something supremely busted about table.insert() and doing #table to get count in MoonSharp
	-- I was getting roughtly 20,000x worse performance using those methods than doing the code below, and didn't want to slow down the test
	MoonSharpStingListCount = 0,
	MoonSharpAddStringToList = function(self, newString) 
		self.MoonSharpStingListCount = self.MoonSharpStingListCount + 1
		self.ListOfStrings[self.MoonSharpStingListCount]	= newString
	end,

	ItterateThroughListAndGetCount = function(self)
		local count = 0	
		for index, string in pairs (self.ListOfStrings) do
			count = count + 1
		end

		return count
	end,
}
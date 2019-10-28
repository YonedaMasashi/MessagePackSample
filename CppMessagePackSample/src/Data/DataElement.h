#pragma once

#include <vector>
#include "msgpack.hpp"

using namespace std;

class DataElement
{
public:
	long lA;
	double X1;
	double Y1;
	double X2;
	double Y2;
	long lS;

	MSGPACK_DEFINE(lA, X1, Y1, X2, Y2, lS);
};

class Piece
{
public:
	long PieceNo;
	vector<DataElement> DataList;

	MSGPACK_DEFINE(PieceNo, DataList);

};

class Pack {
public:
	long PackNo;
	vector<Piece> PieceList;

	MSGPACK_DEFINE(PackNo, PieceList);
};


class Root {
public:
	vector<Pack> PackList;

	MSGPACK_DEFINE(PackList);
};
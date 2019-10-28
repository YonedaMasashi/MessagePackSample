#include <iostream>
#include <fstream>

#include "Data/DataElement.h"

using namespace std;

void setData(Root& root);

int main() {

	// データセット
	Root root;
	setData(root);
	
	// 出力
	msgpack::sbuffer sbuf;
	{
		std::chrono::system_clock::time_point  start, end; // 型は auto で可
		start = std::chrono::system_clock::now();

		msgpack::pack(sbuf, root);

		ofstream myFile("cpp_data.bin", ios::out | ios::binary);
		myFile.write(sbuf.data(), sbuf.size());
		myFile.close();

		end = std::chrono::system_clock::now();
		double elapsed = std::chrono::duration_cast<std::chrono::milliseconds>(end - start).count(); //処理に要した時間をミリ秒に変換

		cout << "■MessagePack 出力:" << elapsed << endl;
	}

	// ファイルからデータを読み込む
	{
		std::chrono::system_clock::time_point  start, end; // 型は auto で可
		start = std::chrono::system_clock::now();

		char* buffer = new char[sbuf.size()];
		ifstream inFile("cpp_data.bin", ios::in | ios::binary);
		inFile.read(buffer, sbuf.size());
		inFile.close();

		msgpack::object_handle oh = msgpack::unpack(buffer, sbuf.size());
		msgpack::object obj = oh.get();

		Root load_root;
		obj.convert(load_root);

		end = std::chrono::system_clock::now();
		double elapsed = std::chrono::duration_cast<std::chrono::milliseconds>(end - start).count(); //処理に要した時間をミリ秒に変換

		cout << "■MessagePack 読込:" << elapsed << endl;
	}

	return 0;
}

void setData(Root& root) {

	for (int packNo = 0; packNo < 25; ++packNo) {
		Pack pack;
		pack.PackNo = packNo + 1;

		for (int pieceNo = 0; pieceNo < 4; ++pieceNo) {
			Piece piece;
			piece.PieceNo = pieceNo + 1;

			for (int dataNo = 0; dataNo < 7500; ++dataNo) {
				DataElement data;
				data.lA = (double)dataNo + ((double)7500 * (double)pieceNo);
				data.X1 = (double)dataNo + ((double)7500 * (double)pieceNo) + 0.1;
				data.Y1 = (double)dataNo + ((double)7500 * (double)pieceNo) + 0.2;
				data.X2 = (double)dataNo + ((double)7500 * (double)pieceNo) + 0.3;
				data.Y2 = (double)dataNo + ((double)7500 * (double)pieceNo) + 0.4;
				data.lS = dataNo % 50;

				piece.DataList.push_back(data);
			}

			pack.PieceList.push_back(piece);
		}
		root.PackList.push_back(pack);

	}

}
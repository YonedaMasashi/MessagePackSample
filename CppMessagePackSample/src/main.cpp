#include <iostream>
#include <fstream>

#include "Data/DataElement.h"

using namespace std;

void setData(Root& root);

int main() {

	// �f�[�^�Z�b�g
	Root root;
	setData(root);
	
	// �o��
	msgpack::sbuffer sbuf;
	{
		std::chrono::system_clock::time_point  start, end; // �^�� auto �ŉ�
		start = std::chrono::system_clock::now();

		msgpack::pack(sbuf, root);

		ofstream myFile("cpp_data.bin", ios::out | ios::binary);
		myFile.write(sbuf.data(), sbuf.size());
		myFile.close();

		end = std::chrono::system_clock::now();
		double elapsed = std::chrono::duration_cast<std::chrono::milliseconds>(end - start).count(); //�����ɗv�������Ԃ��~���b�ɕϊ�

		cout << "��MessagePack �o��:" << elapsed << endl;
	}

	// �t�@�C������f�[�^��ǂݍ���
	{
		std::chrono::system_clock::time_point  start, end; // �^�� auto �ŉ�
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
		double elapsed = std::chrono::duration_cast<std::chrono::milliseconds>(end - start).count(); //�����ɗv�������Ԃ��~���b�ɕϊ�

		cout << "��MessagePack �Ǎ�:" << elapsed << endl;
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
#include "pch.h"
#include "csInterface.h"
#include "ecode.h"
#include "util_fullpath.h"
#include "util_file.h"

csInterface::csInterface()
{
	this->initialize();
}

csInterface::~csInterface()
{
	std::vector<ord_struct>::iterator	iter;
	for (iter = vORD.begin(); iter != vORD.end(); iter++) {
		(*iter).mvParams.clear();
	}
}

int csInterface::initialize()
{
	return this->variables();
}

int csInterface::variables()
{
	vORD.clear();
	return RET_SUCCESS;
}

void csInterface::dbg_save(const char* pFullpath)
{
	// std::vector<ord_struct>	vORD;					//! ORD�\���̔z��
	// std::vector<std::string>	mvParams;				//! ORD���(57�p�����[�^)

	using namespace std;

	vector<ord_struct>::iterator	ord_iter;
	vector<string>::iterator		str_iter;
	string str_params;

	for (ord_iter = vORD.begin(); ord_iter != vORD.end(); ord_iter++) {
		vector<string>& wk_params = (*ord_iter).mvParams;
		// �����Q�Ƃɂ���΁A�ǂ݂₷�������B
		for (str_iter = wk_params.begin(); str_iter != wk_params.end(); str_iter++) {
			str_params += (*str_iter);					// 57�p�����[�^���e�L�X�g1�s�ɖ߂�
		}
	}

	utility::file::writer(pFullpath, str_params);
}
